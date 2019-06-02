using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WandD_nodate.CustomElements;
using WandD_nodate.ViewModels;

using Xamarin.Forms;

namespace WandD_nodate.Views
{
	public class TodayPage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<Grouping<string, NoteVM>> NoteGrouping { get; set; }
        IEnumerable<Grouping<string, NoteVM>> groups;

        CustomListView todayNotesListView;
        //Label nonotesLabel = new Label();
        public static Image nonotesImage = new Image { WidthRequest =200,HeightRequest=200};
        Button addButton = new Button();

        ToolbarItem ToolbarItem = new ToolbarItem();

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged2(string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        public TodayPage()
        {
            Title = "Дела на сегодня";
            ToolbarItem toolbar_edit2 = new ToolbarItem { Icon = "toolbar_edit3.png" };
            toolbar_edit2.Clicked += Toolbar_edit2_Clicked;
            ToolbarItems.Add(ToolbarItem);
            ToolbarItems.Add(toolbar_edit2);
            BindingContext = new NotesViewModel { Navigation = this.Navigation };
            
            todayNotesListView = new CustomListView
            {
                HasUnevenRows = true,
                IsGroupingEnabled = true,
                GroupHeaderTemplate = new DataTemplate(() =>
                {
                    Label headerLabel = new Label
                    {
                        TextColor = Color.FromHex("0D47A1"),
                        FontAttributes = FontAttributes.None,
                        Margin = new Thickness(25, 20, 0, 15)
                    };
                    headerLabel.SetBinding(Label.TextProperty, "Name");
                    return new ViewCell
                    {
                        View = headerLabel
                    };
                }),
                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        Margin=new Thickness(5,0,0,0)
                    };
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    Label commentLabel = new Label();
                    commentLabel.SetBinding(Label.TextProperty, "Comment");
                    commentLabel.SetBinding(Label.IsVisibleProperty, "HasComment");

                    Label sublabel = new Label { IsVisible = false};
                    sublabel.SetBinding(Label.TextProperty, "Subtasks_string", BindingMode.TwoWay);
                    
                    int count_subtasks = 0;
                    StackLayout subtasksSL = new StackLayout
                    {
                        Margin = new Thickness(35, 0, 0, 0),
                        Spacing=10
                    };
                    subtasksSL.SetBinding(StackLayout.IsVisibleProperty, "HasSubtasks");
                    
                    sublabel.BindingContextChanged += (sender, e) =>
                    {
                        string str = sublabel.Text;
                        if (str != null)
                        {
                            string[] subtasks_array = str.Split(new char[] { '✖' });
                            foreach (char s in str)
                            {
                                if (s == '✖')
                                    count_subtasks++;
                            }

                            for (int i = 1; i <= count_subtasks; i++)
                            {
                                int subtask_index=i - 1;
                                CustomButton donesubtaskButton = new CustomButton
                                {
                                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                                    WidthRequest = 23,
                                    HeightRequest = 23,
                                    BorderRadius = 12,
                                    BackgroundColor = Color.Transparent,
                                    BorderWidth =1,
                                    BorderColor = Color.LightGray,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Start
                                };
                                donesubtaskButton.SetBinding(Button.TextColorProperty, "TextColorSubtasks");
                                donesubtaskButton.SetBinding(Button.BorderColorProperty, "ColorSubtasks");

                                Label newsubtaskLabel = new Label
                                {
                                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry)),
                                    Text = subtasks_array[i - 1]
                                };
                                
                                if (Addition.IsStrikethrough(newsubtaskLabel.Text) == true)
                                    donesubtaskButton.Text = "✔";

                                StackLayout newsubtaskSL = new StackLayout
                                {
                                    Children = { donesubtaskButton, newsubtaskLabel },
                                    Orientation = StackOrientation.Horizontal
                                };
                                subtasksSL.Children.Add(newsubtaskSL);
                                
                                donesubtaskButton.Clicked += async(sender2, e2) => 
                                {
                                    string substringOld = newsubtaskLabel.Text;
                                    if (Addition.IsStrikethrough(newsubtaskLabel.Text) == true)
                                    {
                                        donesubtaskButton.Text = "";
                                        newsubtaskLabel.Text = Addition.ConvertFromStrikethrough(newsubtaskLabel.Text);
                                    }
                                    else
                                    {
                                        donesubtaskButton.Text = "✔";
                                        newsubtaskLabel.Text = Addition.ConvertToStrikethrough(newsubtaskLabel.Text);
                                    }
                                    subtasks_array[subtask_index] = newsubtaskLabel.Text;
                                    str = String.Join("✖", subtasks_array);
                                    sublabel.Text = str;
                                    
                                    var note = sublabel?.BindingContext as NoteVM;
                                    await App.Database.SaveItemAsync(note);
                                };
                            }
                        }
                    };

                    Label everydayLabel = new Label
                    {
                        Text = "Каждый день",
                        FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
                    };
                    everydayLabel.SetBinding(Label.IsVisibleProperty, "Repeat");

                    Label overdueLabel = new Label { FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)) };
                    overdueLabel.SetBinding(Label.TextProperty, "Overdue_str");
                    overdueLabel.SetBinding(Label.IsVisibleProperty, "IsOverdue");
                    
                    CustomButton doneButton = new CustomButton
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                        WidthRequest = 30,
                        HeightRequest = 30,
                        BorderRadius = 15,
                        BorderColor = Color.LightGray,
                        BorderWidth = 1,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Start,
                        Margin=new Thickness(0,1,0,0)
                    };
                    doneButton.SetBinding(Button.BorderWidthProperty, "ButtonsWidth");
                    doneButton.SetBinding(BackgroundColorProperty, "ColorMarker");
                    doneButton.Clicked += Done_Clicked;
                    doneButton.Clicked += (sender, e) =>
                    {
                        nameLabel.Text = Addition.ConvertToStrikethrough(nameLabel.Text);
                    };

                    Button removeButton = new Button
                    {
                        Text = "✖",
                        BackgroundColor = Color.White,
                        TextColor = Color.FromHex("0D47A1")
                    };
                    removeButton.Clicked += RemoveButton_Clicked;

                    Button editButton = new Button
                    {
                        Text = "•••",
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Color.White,
                        TextColor = Color.FromHex("0D47A1")
                    };
                    editButton.Clicked += edit_Clicked;

                    StackLayout invisibleStackL = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Margin = new Thickness(0, 10, 0, 0),
                        Padding = new Thickness(-20, 0, 0, 0),
                        Children = { /*doneButton,*/ removeButton, editButton }
                    };
                    invisibleStackL.SetBinding(StackLayout.IsVisibleProperty, "IsVisible");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            Children =
                        {
                                new StackLayout
                                {   Orientation = StackOrientation.Horizontal,
                                    Children =
                                    {
                                        doneButton,
                                        new StackLayout
                                        {
                                            Children=
                                            {
                                                nameLabel,
                                                new StackLayout
                                                {
                                                    Orientation = StackOrientation.Horizontal,
                                                    Children ={everydayLabel,overdueLabel},
                                                    Margin=new Thickness(5,0,0,0)
                                                },
                                            },
                                        }
                                    }
                                },
                                commentLabel,
                                sublabel,
                                subtasksSL,
                                invisibleStackL
                        },
                            Margin = new Thickness(10),
                            Padding = new Thickness(20, 0, 10, 0),
                            Spacing = 15,
                        }
                    };
                })
            };
            todayNotesListView.ItemTapped += todayNotesListView_ItemTapped;

            addButton = new Button
            {
                Text = "+",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BackgroundColor = Color.FromHex("E3F2FD"),
                TextColor = Color.FromHex("0D47A1"),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                WidthRequest = 70,
                HeightRequest = 70,
                BorderRadius = 35,
                Margin = new Thickness(0, 0, 20, 20)
            };
            addButton.SetBinding(Button.CommandProperty, "CreateNoteCommand");

            RelativeLayout relativeLayout = new RelativeLayout();
            relativeLayout.Children.Add(todayNotesListView, 
                Constraint.RelativeToParent((parent) => parent.X), 
                Constraint.RelativeToParent((parent) =>parent.Y/* + 20*/), 
                Constraint.RelativeToParent((parent) =>parent.Width), 
                Constraint.RelativeToParent((parent) =>parent.Height) );

            //double getnonotesLabelWidth(RelativeLayout parent) => nonotesLabel.Measure(parent.Width, parent.Height).Request.Width;
            //relativeLayout.Children.Add(nonotesLabel,
            //    Constraint.RelativeToParent(parent => parent.Width / 2 - getnonotesLabelWidth(parent) / 2),
            //    Constraint.RelativeToParent(parent => parent.Height*.45));
            double getnonotesImageWidth(RelativeLayout parent) => nonotesImage.Measure(parent.Width, parent.Height).Request.Width;
            double getnonotesImageHeight(RelativeLayout parent) => nonotesImage.Measure(parent.Width, parent.Height).Request.Width;
            relativeLayout.Children.Add(nonotesImage,
                Constraint.RelativeToParent(parent =>/*parent.Width/2- 100*/ parent.Width / 2 - getnonotesImageWidth(parent) / 2),
                Constraint.RelativeToParent(parent => parent.Height / 2 - getnonotesImageHeight(parent) / 2/*parent.Height/2-100*/ /*parent.Height * .45*/));

            relativeLayout.Children.Add(addButton, 
                Constraint.RelativeToParent((parent) =>parent.Width * .75),
                Constraint.RelativeToParent((parent) =>parent.Height * .8125) );

            this.BackgroundColor = Color.White;
            this.Content = relativeLayout;
        }

        private async void Toolbar_edit2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            nonotesImage.IsVisible = false;
            nonotesImage.Source = Addition.ChooseImage();
        }

        protected override async void OnAppearing()
        {
            await App.Database.Update();

            //закрытие раскрытой ячейки
            if (NotesViewModel.oldNote != null)
            {
                NotesViewModel.oldNote.IsVisible = false;
                NotesViewModel.UpdateNotes(NotesViewModel.oldNote);
            }
            
            //получаем заметки на сегодня и просроченные
            var todayNotes = await App.Database.TodayNotesAsync();
            if (todayNotes.Count != 0)
            {
                //сортировка
                var sorted = todayNotes.OrderBy(u => u.Date);
                //получаем группы  и группируем
                groups = sorted.GroupBy(p => p.Date_str).Select(g => new Grouping<string, NoteVM>(g.Key, g));
                //передаем группы в NoteGroups
                NoteGrouping = new ObservableCollection<Grouping<string, NoteVM>>(groups);
                //передаем отсортированный и сгруппированный список
                todayNotesListView.ItemsSource = NoteGrouping;

                nonotesImage.IsVisible = false;
                //nonotesLabel.IsVisible = false;
                todayNotesListView.IsVisible = true;
            }
            else
            {
                //    nonotesImage.Source = Addition.ChooseImage();
                nonotesImage.IsVisible = true;
                //nonotesLabel.IsVisible = true;
                todayNotesListView.IsVisible = false;
            }

            base.OnAppearing();
            ToolbarItem.Icon = NotesViewModel.CountNotes();
        }

        private void todayNotesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            todayNotesListView.SelectedItem = null;
            var note = e.Item as NoteVM;
            var vm = BindingContext as NotesViewModel;
            vm?.HideorShowNotes(note);
        }

        private void Done_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var note = button?.BindingContext as NoteVM;
            button.Text = "✔";

            var vm = BindingContext as NotesViewModel;
            vm?.RemoveCommand.Execute(note);
            OnAppearing();
        }

        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var note = button?.BindingContext as NoteVM;
            if (note != null)
            {
                await App.Database.DeleteItemAsync(note);
            }
            OnAppearing();
        }

        private void edit_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var note = button?.BindingContext as NoteVM;

            var vm = BindingContext as NotesViewModel;
            vm?.SelectedNote(note);
        }

       
    }
}