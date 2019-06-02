using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WandD_nodate.Views_UWP;
using WandD_nodate.ViewModels;
using Xamarin.Forms;
using Windows.UI.Xaml.Input;
using System.Threading;
using WandD_nodate.CustomElements;

namespace WandD_nodate
{
    public class MainPage_UWP : ContentPage
	{
        public ObservableCollection<Grouping<string, NoteVM>> NoteGrouping { get; set; }
        IEnumerable<Grouping<string, NoteVM>> groups;
        ListView todayNotesListView;
        ListView notesListView;
        public static Image addImage;
        Label nonotesLabel = new Label { Text = "Нет заметок", FontFamily = "Open Sans", };
        Image countImage =new Image{ HeightRequest=30, WidthRequest=30, Margin = new Thickness(5, 0, 0, 0)};
        Label righttitle = new Label
        {
            Text = "Заметки на сегодня",
            FontFamily = "Open Sans",
            TextColor = Color.White,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            Margin = new Thickness(20, 0, 0, 0)
        };
        List<NoteVM> todayNotes;
        List<NoteVM> allnotes;
        //bool tap = true;
        int page = 1;
        public static Image nodateImage = new Image();
        StackLayout nodateSL;
        public static double newnoteSLSize;

        public MainPage_UWP()
        {
            //var underlineLabel = new Label { Text = "This is underlined text.", TextDecorations = TextDecorations.Underline };

            nodateSL = new StackLayout
            {
                Children = { nodateImage },
                HeightRequest = 300,
                WidthRequest = 350,
                //BackgroundColor = Color.Gray
            };
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new NotesViewModel { Navigation = this.Navigation };

            /////////ЛЕВАЯ ЧАСТЬ ЭКРАНА//////////
            StackLayout todaySL_inleft = new StackLayout
            {
                Children =
                        {
                            new Image { Source = "images/today_white.png", HeightRequest=20, WidthRequest=20 },
                            new Label
                            {
                                Text="Сегодня",
                                FontFamily = "Open Sans",
                                FontSize=Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                VerticalOptions=LayoutOptions.CenterAndExpand,
                                TextColor=Color.White,
                                Margin=new Thickness(5,0,0,0)
                            }
                        },
                Orientation = StackOrientation.Horizontal,
                //Margin=new Thickness(0,15,0,0),
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Color.Accent
            };
            StackLayout allSL_inleft = new StackLayout
            {
                Children =
                        {
                            new Image { Source = "images/all_blue2.png", HeightRequest=20, WidthRequest=20 },
                            new Label
                            {
                                Text="Все",
                                FontFamily = "Open Sans",
                                FontSize=Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                VerticalOptions=LayoutOptions.CenterAndExpand,
                                Margin=new Thickness(5,0,0,0)
                            }
                        },
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Color.Transparent
            };
            todaySL_inleft.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (page == 2)
                    {
                        page = 1;
                        righttitle.Text = "Заметки на cегодня";
                        NewNotePage_UWP.newnoteSL.IsVisible = false;
                        allSL_inleft.BackgroundColor = Color.Transparent;
                        (allSL_inleft.Children[1] as Label).TextColor = Color.Black;
                        (allSL_inleft.Children[0] as Image).Source = "images/all_blue2.png";
                        todaySL_inleft.BackgroundColor = Color.Accent;
                        (todaySL_inleft.Children[1] as Label).TextColor = Color.White;
                        (todaySL_inleft.Children[0] as Image).Source = "images/today_white.png";
                        addImage.Source = "images/plus.png";

                        nodateImage.Source = "images/" + Addition.ChooseImage();

                        OnAppearing();
                    }
                })
            });
            allSL_inleft.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (page == 1)
                    {
                        page = 2;
                        righttitle.Text = "Все заметки";
                        NewNotePage_UWP.newnoteSL.IsVisible = false;
                        todaySL_inleft.BackgroundColor = Color.Transparent;
                        (todaySL_inleft.Children[1] as Label).TextColor = Color.Black;
                        (todaySL_inleft.Children[0] as Image).Source = "images/today_blue.png";
                        allSL_inleft.BackgroundColor = Color.Accent;
                        (allSL_inleft.Children[1] as Label).TextColor = Color.White;
                        (allSL_inleft.Children[0] as Image).Source = "images/all_white.png";
                        addImage.Source = "images/plus.png";

                        nodateImage.Source = "images/" + Addition.ChooseImage();
                        OnAppearing();
                    }
                })
            });
            StackLayout otherimageSL = new StackLayout
            {
                Children =
                {
                    new Image
                    {
                        Source="images/options_blue4.png",
                        Aspect=Aspect.AspectFit,
                        HorizontalOptions=LayoutOptions.EndAndExpand,
                        VerticalOptions=LayoutOptions.CenterAndExpand,
                        HeightRequest=15,
                        WidthRequest=15,
                    }
                },
                Margin = new Thickness(0, 10, 0, 0),
                BackgroundColor = Color.Transparent
            };
            otherimageSL.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (Settings_UWP.otherSL.IsVisible == true)
                    {
                        Settings_UWP.otherSL.IsVisible = false;
                        (todaySL_inleft.Children[1] as Label).Text = "";
                        (allSL_inleft.Children[1] as Label).Text = "";
                        todaySL_inleft.Padding = new Thickness(10, 10, 2, 10);
                        allSL_inleft.Padding = new Thickness(10, 10, 2, 10);
                        //(otherimageSL.Children[0] as Image).HorizontalOptions = LayoutOptions.Center;
                        (otherimageSL.Children[0] as Image).Source = "images/options_blue3.png";
                    }
                    else
                    {
                        Settings_UWP.otherSL.IsVisible = true;
                        (todaySL_inleft.Children[1] as Label).Text = "Сегодня";
                        (allSL_inleft.Children[1] as Label).Text = "Все";
                        todaySL_inleft.Padding = new Thickness(10, 10, 10, 10);
                        allSL_inleft.Padding = new Thickness(10, 10, 10, 10);
                        (otherimageSL.Children[0] as Image).Source = "images/options_blue4.png";
                    }
                })
            });
            Settings_UWP.showoverdueSwitch_today.Toggled += Settings_UWP.ShowoverdueSwitchToday_Toggled;
            Settings_UWP.showoverdueSwitch_today.Toggled += Refresh_Clicked;
            Settings_UWP.showoverdueSwitch_all.Toggled += Settings_UWP.ShowoverdueSwitchALL_Toggled;
            Settings_UWP.showoverdueSwitch_all.Toggled += Refresh_Clicked;
            StackLayout leftSL = new StackLayout
            {
                Children =
                {
                    //верхняя синяя полоса
                    new StackLayout
                    {
                        Children={countImage},
                        HeightRequest=47.5,
                        BackgroundColor=Color.FromHex("1564c0"),
                        Orientation=StackOrientation.Horizontal
                    },
                    todaySL_inleft,
                    allSL_inleft,
                    //new BoxView
                    //{
                    //    HeightRequest = 1 ,
                    //    BackgroundColor =Color.Gray
                    //},
                    //еще
                    new StackLayout
                    {
                        Children=
                        {
                            otherimageSL,
                            Settings_UWP.otherSL
                        },
                        HeightRequest=40,
                        BackgroundColor=Color.Transparent,
                        Padding = new Thickness(10, 10, 10, 0),
                    }
                },
                BackgroundColor = Color.FromHex("EEEEEE"),
                Spacing = 0
            };

            ////////ПРАВАЯ ЧАСТЬ ЭКРАНА////////////////////
            todayNotesListView = new ListView
            {
                Margin = new Thickness(0, 10, 0, 40),
                HasUnevenRows = true,
                IsGroupingEnabled = true,
                GroupHeaderTemplate = new DataTemplate(() =>
                {
                    Label headerLabel = new Label
                    {
                        //TextColor=Color.DarkSlateGray,
                        TextColor = Color.FromHex("0D47A1"),
                        FontSize = 25,
                        FontAttributes = FontAttributes.None,
                        Margin = new Thickness(40, 5, 40, 0)
                    };
                    headerLabel.SetBinding(Label.TextProperty, "Name");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Children =
                            {
                                headerLabel,
                                new BoxView
                                {
                                    HeightRequest=1,
                                    BackgroundColor=Color.LightGray,
                                    Margin=new Thickness(0,5,0,0)
                                }
                            },
                        }
                    };
                }),
                ItemTemplate = new DataTemplate(() =>
                {
                    Button removeButton = new Button
                    {
                        //BackgroundColor = Color.FromHex("E3F2FD"),
                        BorderColor = Color.LightGray,
                        BorderWidth = 0.75,
                        TextColor = Color.FromHex("0D47A1"),
                        FontFamily = "Segoe MDL2 Assets",
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        WidthRequest = 35,
                        HeightRequest = 35,
                        BorderRadius = 17,
                    };
                    removeButton.SetBinding(Button.BorderWidthProperty, "ButtonsWidth");
                    removeButton.SetBinding(BackgroundColorProperty, "ColorMarker");
                    removeButton.Clicked += Remove_Clicked;

                    Label nameLabel1 = new Label
                    {
                        FontFamily = "Open Sans",
                        TextColor = Color.Gray, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Margin = new Thickness(5, 0, 0, 0)
                    };
                    nameLabel1.SetBinding(Label.TextProperty, "Name");

                    Label everydayLabel = new Label
                    {
                        FontFamily = "Open Sans",
                        Text = "Каждый день",
                        FontSize = 12.5
                    };
                    everydayLabel.SetBinding(Label.IsVisibleProperty, "Repeat");

                    Label overdueLabel = new Label
                    {
                        FontFamily = "Open Sans",
                        FontSize = 12.5
                    };
                    overdueLabel.SetBinding(Label.TextProperty, "Overdue_str");
                    overdueLabel.SetBinding(Label.IsVisibleProperty, "IsOverdue");

                    Label commentLabel = new Label
                    {
                        FontFamily = "Open Sans",
                        Margin = new Thickness(5, 0, 0, 0),
                        FontSize = 20
                    };
                    commentLabel.SetBinding(Label.TextProperty, "Comment");
                    commentLabel.SetBinding(Label.IsVisibleProperty, "HasComment");

                    Label sublabel = new Label { IsVisible = false};
                    sublabel.SetBinding(Label.TextProperty, "Subtasks_string", BindingMode.TwoWay);

                    int count_subtasks = 0;
                    StackLayout subtasksSL = new StackLayout
                    {
                        Margin = new Thickness(30, 7.5, 0, 0),
                        Spacing = 10
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
                                int subtask_index = i - 1;
                                CustomButton donesubtaskButton = new CustomButton
                                {
                                    FontFamily = "Segoe MDL2 Assets",
                                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                                    WidthRequest = 23,
                                    HeightRequest = 23,
                                    BorderRadius = 12,
                                    BackgroundColor = Color.Transparent,
                                    BorderWidth = 1,
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

                                //if (Addition.IsStrikethrough(newsubtaskLabel.Text) == true)
                                //    donesubtaskButton.Text = "\uE73E";
                                if (subtasks_array[i - 1].StartsWith("➥") == true)
                                {
                                    donesubtaskButton.Text = "\uE73E";
                                    newsubtaskLabel.Text = newsubtaskLabel.Text.Remove(0, 1);
                                    newsubtaskLabel.TextDecorations = TextDecorations.Strikethrough;
                                }

                                StackLayout newsubtaskSL = new StackLayout
                                {
                                    Children = { donesubtaskButton, newsubtaskLabel },
                                    Orientation = StackOrientation.Horizontal
                                };
                                subtasksSL.Children.Add(newsubtaskSL);

                                string str2 = subtasks_array[i - 1];
                                donesubtaskButton.Clicked += async (sender2, e2) =>
                                {
                                    if (newsubtaskLabel.TextDecorations == TextDecorations.Strikethrough)
                                    {
                                        donesubtaskButton.Text = "";
                                        newsubtaskLabel.TextDecorations = TextDecorations.None;
                                        subtasks_array[i - 1] = str2.Remove(0, 1);
                                    }
                                    else
                                    {
                                        donesubtaskButton.Text = "\uE73E";
                                        newsubtaskLabel.TextDecorations = TextDecorations.Strikethrough;
                                        subtasks_array[i - 1] = "➥" + str2;
                                    }
                                    //if (Addition.IsStrikethrough(newsubtaskLabel.Text) == true)
                                    //{
                                    //    donesubtaskButton.Text = "";
                                    //    newsubtaskLabel.Text = Addition.ConvertFromStrikethrough(newsubtaskLabel.Text);
                                    //}
                                    //else
                                    //{
                                    //    donesubtaskButton.Text = "\uE73E";
                                    //    newsubtaskLabel.Text = Addition.ConvertToStrikethrough(newsubtaskLabel.Text);
                                    //}

                                    //subtasks_array[subtask_index] = newsubtaskLabel.Text;
                                    subtasks_array[subtask_index] = subtasks_array[i - 1];
                                    str = String.Join("✖", subtasks_array);
                                    sublabel.Text = str;

                                    var note = sublabel?.BindingContext as NoteVM;
                                    await App.Database.SaveItemAsync(note);
                                };
                            }
                        }
                    };

                    Grid listview_grid = new Grid
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Auto }
                        },
                        ColumnSpacing = 15
                    };
                    listview_grid.Children.Add(removeButton, 0, 0);
                    listview_grid.Children.Add(new StackLayout
                    {
                        Children =
                        {
                            nameLabel1,
                            new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children = { everydayLabel, overdueLabel },
                                Margin = new Thickness(5, 0, 0, 0)
                            },
                           commentLabel,
                           sublabel,
                           subtasksSL,
                        }
                    }, 1, 0);

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Children = { listview_grid },
                            Padding = new Thickness(65, 12.5, 15, 12.5),
                            Spacing = 15,
                        }
                    };
                })
            };
            todayNotesListView.IsVisible = true;
            todayNotesListView.ItemTapped += ListView_ItemTapped;

            notesListView = new ListView
            {
                Margin = new Thickness(0, 10, 0, 40),
                HasUnevenRows = true,
                IsGroupingEnabled = true,
                GroupHeaderTemplate = new DataTemplate(() =>
                {
                    Label headerLabel = new Label
                    {
                        TextColor = Color.FromHex("0D47A1"),
                        FontSize = 25,
                        FontAttributes = FontAttributes.None,
                        Margin = new Thickness(40, 5, 40, 0)
                    };
                    headerLabel.SetBinding(Label.TextProperty, "Name");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Children =
                            {
                                headerLabel,
                                new BoxView
                                {
                                    HeightRequest =1,
                                    //BackgroundColor =Color.FromHex("EEEEEE"),
                                    BackgroundColor=Color.LightGray,
                                    Margin=new Thickness(0,5,0,0)
                                }
                            },
                        }
                    };
                }),
                ItemTemplate = new DataTemplate(() =>
                {
                    Button removeButton = new Button
                    {
                        //BackgroundColor = Color.FromHex("E3F2FD"),
                        BorderColor = Color.LightGray,
                        BorderWidth = 0.75,
                        TextColor = Color.FromHex("0D47A1"),
                        FontFamily = "Segoe MDL2 Assets",
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        WidthRequest = 35,
                        HeightRequest = 35,
                        BorderRadius = 17,
                    };
                    removeButton.SetBinding(Button.BorderWidthProperty, "ButtonsWidth");
                    removeButton.SetBinding(BackgroundColorProperty, "ColorMarker");
                    removeButton.Clicked += Remove_Clicked;

                    Label nameLabel1 = new Label
                    {
                        FontFamily = "Open Sans",
                        TextColor = Color.Gray,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Margin = new Thickness(5, 0, 0, 0)
                    };
                    nameLabel1.SetBinding(Label.TextProperty, "Name");

                    Label everydayLabel = new Label
                    {
                        FontFamily = "Open Sans",
                        Text = "Каждый день",
                        FontSize = 12.5
                    };
                    everydayLabel.SetBinding(Label.IsVisibleProperty, "Repeat");

                    Label overdueLabel = new Label
                    {
                        FontFamily = "Open Sans",
                        FontSize = 12.5
                    };
                    overdueLabel.SetBinding(Label.TextProperty, "Overdue_str");
                    overdueLabel.SetBinding(Label.IsVisibleProperty, "IsOverdue");

                    Label commentLabel = new Label
                    {
                        FontFamily = "Open Sans",
                        Margin = new Thickness(5, 0, 0, 0),
                        FontSize = 20
                    };
                    commentLabel.SetBinding(Label.TextProperty, "Comment");
                    commentLabel.SetBinding(Label.IsVisibleProperty, "HasComment");

                    Label sublabel = new Label { IsVisible = false };
                    sublabel.SetBinding(Label.TextProperty, "Subtasks_string", BindingMode.TwoWay);

                    int count_subtasks = 0;
                    StackLayout subtasksSL = new StackLayout
                    {
                        Margin = new Thickness(30, 7.5, 0, 0),
                        Spacing = 10
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
                                int subtask_index = i - 1;
                                CustomButton donesubtaskButton = new CustomButton
                                {
                                    FontFamily = "Segoe MDL2 Assets",
                                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                                    WidthRequest = 23,
                                    HeightRequest = 23,
                                    BorderRadius = 12,
                                    BackgroundColor = Color.Transparent,
                                    BorderWidth = 1,
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

                                //if (Addition.IsStrikethrough(newsubtaskLabel.Text) == true)
                                //    donesubtaskButton.Text = "\uE73E";
                                if (subtasks_array[i - 1].StartsWith("➥") == true)
                                {
                                    donesubtaskButton.Text = "\uE73E";
                                    newsubtaskLabel.Text = newsubtaskLabel.Text.Remove(0, 1);
                                    newsubtaskLabel.TextDecorations = TextDecorations.Strikethrough;
                                }

                                StackLayout newsubtaskSL = new StackLayout
                                {
                                    Children = { donesubtaskButton, newsubtaskLabel },
                                    Orientation = StackOrientation.Horizontal
                                };
                                subtasksSL.Children.Add(newsubtaskSL);

                                string str2 = subtasks_array[i - 1];
                                donesubtaskButton.Clicked += async (sender2, e2) =>
                                {
                                    if (newsubtaskLabel.TextDecorations == TextDecorations.Strikethrough)
                                    {
                                        donesubtaskButton.Text = "";
                                        newsubtaskLabel.TextDecorations = TextDecorations.None;
                                        subtasks_array[i - 1] = str2.Remove(0, 1);
                                    }
                                    else
                                    {
                                        donesubtaskButton.Text = "\uE73E";
                                        newsubtaskLabel.TextDecorations = TextDecorations.Strikethrough;
                                        subtasks_array[i - 1] = "➥" + str2;
                                    }
                                    //if (Addition.IsStrikethrough(newsubtaskLabel.Text) == true)
                                    //{
                                    //    donesubtaskButton.Text = "";
                                    //    newsubtaskLabel.Text = Addition.ConvertFromStrikethrough(newsubtaskLabel.Text);
                                    //}
                                    //else
                                    //{
                                    //    donesubtaskButton.Text = "\uE73E";
                                    //    newsubtaskLabel.Text = Addition.ConvertToStrikethrough(newsubtaskLabel.Text);
                                    //}
                                    //subtasks_array[subtask_index] = newsubtaskLabel.Text;
                                    subtasks_array[subtask_index] = subtasks_array[i - 1];
                                    str = String.Join("✖", subtasks_array);
                                    sublabel.Text = str;

                                    var note = sublabel?.BindingContext as NoteVM;
                                    await App.Database.SaveItemAsync(note);
                                };
                            }
                        }
                    };

                    Grid listview_grid = new Grid
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Auto }
                        },
                        ColumnSpacing = 15
                    };
                    listview_grid.Children.Add(removeButton, 0, 0);
                    listview_grid.Children.Add(new StackLayout
                    {
                        Children =
                        {
                            nameLabel1,
                            new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children = { everydayLabel, overdueLabel },
                                Margin = new Thickness(5, 0, 0, 0)
                            },
                           commentLabel,
                           sublabel,
                           subtasksSL,
                        }
                    }, 1, 0);

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Children = { listview_grid },
                            Padding = new Thickness(65, 12.5, 15, 12.5),
                            Spacing = 15,
                        }
                    };
                })
            };
            notesListView.IsVisible = false;
            notesListView.ItemTapped += ListView_ItemTapped;

            ////добавление или редактирование заметки
            NewNotePage_UWP.nameEntry.TextChanged += NewNotePage_UWP.NameEntry_TextChanged;
            NewNotePage_UWP.addnewnoteButton.Clicked += NewNotePage_UWP.addnewnoteButton_Clicked;
            NewNotePage_UWP.addnewnoteButton.Clicked += Refresh_Clicked;
            NewNotePage_UWP.removeButton.Clicked += NewNotePage_UWP.removeButton_Clicked;
            NewNotePage_UWP.removeButton.Clicked += Refresh_Clicked;
            NewNotePage_UWP.backButton.Clicked += NewNotePage_UWP.BackButton_Clicked;

            NewNotePage_UWP.subtaskLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    NewNotePage_UWP.count_subtasks++;

                    CustomButton donesubtaskButton = new CustomButton
                    {
                        FontSize = 15,
                        WidthRequest = 23,
                        FontFamily = "Segoe MDL2 Assets",
                        HeightRequest = 23,
                        BorderRadius = 12,
                        BackgroundColor = Color.White,
                        BorderWidth = 1,
                        BorderColor = Color.Gray,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 5, 0, 0)
                    };
                    CustomEditor2 newsubtaskEditor = new CustomEditor2
                    {
                        Placeholder = "Введите название",
                        FontFamily = "Open Sans",
                        TextColor = Color.FromHex("0D47A1"),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        AutoSize = EditorAutoSizeOption.TextChanges,
                        WidthRequest = 282,
                        BackgroundColor = Color.Transparent
                    };
                    Label removesubtaskLabel = new Label
                    {
                        FontFamily = "Segoe MDL2 Assets",
                        Text = "\uE894",
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(-6, 7, 0, 0),
                        TextColor = Color.Gray,
                        WidthRequest = 35,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Start,
                    };
                    StackLayout newsubtaskSL = new StackLayout
                    {
                        Children = { donesubtaskButton, newsubtaskEditor, removesubtaskLabel },
                        Orientation = StackOrientation.Horizontal,
                        Margin = 0
                    };
                    newsubtaskEditor.TextChanged += (sender, e) =>
                    {
                        var element = sender as CustomEditor2;
                        if (String.IsNullOrWhiteSpace(element.Text) == false)
                        {
                            element.Text = element.Text.Substring(0, 1).ToUpper() + element.Text.Remove(0, 1);
                        }
                        else
                        {
                            donesubtaskButton.Text = "";
                            element.Text = element.Text.TrimStart();
                        }
                    };

                    if (NewNotePage_UWP.subtasksSL.Children[0] == NewNotePage_UWP.subdefaultSL)
                        NewNotePage_UWP.subtasksSL.Children.Remove(NewNotePage_UWP.subdefaultSL);
                    if (NewNotePage_UWP.subtasksSL.Children.Count > 1)
                        NewNotePage_UWP.subtasksSL.Children.Remove(NewNotePage_UWP.subdefaultSL);

                    NewNotePage_UWP.subtasksSL.Children.Add(newsubtaskSL);
                    NewNotePage_UWP.subtasksSL.Children.Add(NewNotePage_UWP.subdefaultSL);

                    donesubtaskButton.Clicked += (sender, e) =>
                    {
                        if (String.IsNullOrWhiteSpace(newsubtaskEditor.Text) == false)
                        {
                            if (donesubtaskButton.Text == "\uE73E")
                            //if (Addition.IsStrikethrough(newsubtaskEditor.Text) == true)
                            {
                                donesubtaskButton.Text = "";
                                //newsubtaskEditor.Text = Addition.ConvertFromStrikethrough(newsubtaskEditor.Text);
                            }
                            else
                            {
                                donesubtaskButton.Text = "\uE73E";
                                //newsubtaskEditor.Text = Addition.ConvertToStrikethrough(newsubtaskEditor.Text);
                            }
                        }
                    };
                    removesubtaskLabel.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() => { NewNotePage_UWP.count_subtasks--; NewNotePage_UWP.subtasksSL.Children.Remove(newsubtaskSL); })
                    });
                })
            });

            //NewNotePage_UWP.newnoteSL.Content = NewNotePage_UWP.StackLayout;
            NewNotePage_UWP.StackLayout.SizeChanged += NewNotePage_UWP.StackLayout_SizeChanged;
            NewNotePage_UWP.newnoteSL.SizeChanged += NewNotePage_UWP.NewnoteSL_SizeChanged;
            //newnoteSLSize = NewNotePage_UWP.newnoteSL.ContentSize.Height;

            NewNotePage_UWP.colorButton0.Clicked += NewNotePage_UWP.ColorButton_Clicked;
            NewNotePage_UWP.colorButton1.Clicked += NewNotePage_UWP.ColorButton_Clicked;
            NewNotePage_UWP.colorButton2.Clicked += NewNotePage_UWP.ColorButton_Clicked;
            NewNotePage_UWP.colorButton3.Clicked += NewNotePage_UWP.ColorButton_Clicked;
            NewNotePage_UWP.colorButton4.Clicked += NewNotePage_UWP.ColorButton_Clicked;
            NewNotePage_UWP.colorButton5.Clicked += NewNotePage_UWP.ColorButton_Clicked;
            NewNotePage_UWP.colorButton6.Clicked += NewNotePage_UWP.ColorButton_Clicked;
            NewNotePage_UWP.colorButton7.Clicked += NewNotePage_UWP.ColorButton_Clicked;

            addImage = new Image
            {
                Source = "images/plus.png",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 30,
                WidthRequest = 30,
                Margin = new Thickness(0, 15, 10, 15),
                BackgroundColor = Color.Transparent
            };
            addImage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (NewNotePage_UWP.newnoteSL.IsVisible == true)
                    {
                        addImage.Source = "images/plus.png";
                        NewNotePage_UWP.newnoteSL.IsVisible = false;
                        NewNotePage_UWP.Common();
                        //NewNotePage_UWP.count_subtasks = 0;
                        //(NewNotePage_UWP.newnoteSL.Children[0] as Label).Text =
                        //NewNotePage_UWP.count_subtasks.ToString();

                    }
                    else
                    {
                        //addImage.Margin = new Thickness(0, 15, 10, 15);
                        addImage.Source = "images/cross.png";
                        NewNotePage_UWP.nameEntry.Text = "";
                        NewNotePage_UWP.addnewnoteButton.BackgroundColor = Color.White;
                        NewNotePage_UWP.addnewnoteButton.TextColor = Color.FromHex("0D47A1");
                        NewNotePage_UWP.newnoteSL.IsVisible = true;
                        NewNotePage_UWP.datePicker.MinimumDate = DateTime.Today;

                        NewNotePage_UWP.colorButton0.Text = "\uE73E";
                        NewNotePage_UWP.lastButton_int = NewNotePage_UWP.colorButton0.GetHashCode();
                        NewNotePage_UWP.additionButton.BackgroundColor = NewNotePage_UWP.colorButton0.BackgroundColor;
                        NewNotePage_UWP.buttonsStackLayout.HorizontalOptions = LayoutOptions.CenterAndExpand;


                        NewNotePage_UWP.count_subtasks = 0;
                        NewNotePage_UWP.str = null;
                        NewNotePage_UWP.str_old = null;
                        NewNotePage_UWP.subtasksSL.Children.Clear();
                        NewNotePage_UWP.subtasksSL.Children.Add(NewNotePage_UWP.subdefaultSL);
                        NewNotePage_UWP.commentEntry.Text=null;

                        //(NewNotePage_UWP.newnoteSL.Children[0] as Label).Text = 
                        //"сount: " + NewNotePage_UWP.count_subtasks.ToString() + "\n" +
                        //"str: " + NewNotePage_UWP.str + "\n" +
                        //"str_old: " + NewNotePage_UWP.str_old+ "\n"+
                        //"count_children: " + NewNotePage_UWP.subtasksSL.Children.Count();
                    }
                })
            });
            /////////////////////////

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(todayNotesListView,
                Constraint.RelativeToParent((parent) => parent.X),
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height));

            relativeLayout.Children.Add(notesListView,
                Constraint.RelativeToParent((parent) => parent.X),
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height));


            //double getnodateSLWidth(RelativeLayout parent) => nodateSL.Measure(parent.Width, parent.Height).Request.Width;
            //double getnodateSLHeight(RelativeLayout parent) => nodateSL.Measure(parent.Width, parent.Height).Request.Height;
            relativeLayout.Children.Add(nodateSL,
                Constraint.RelativeToParent(parent => this.Width/3.3 /*/ 2 - getnodateSLWidth(parent) / 2.9*/),
                Constraint.RelativeToParent(parent => this.Height/4 /*/ 2 - getnodateSLHeight(parent) /2*/),
                 Constraint.RelativeToParent((parent) => 350),
                Constraint.RelativeToParent((parent) => 300)
                );

            //double getnodateImageWidth(RelativeLayout parent) => nodateImage.Measure(parent.Width, parent.Height).Request.Width;
            //double getnodateImageHeight(RelativeLayout parent) => nodateImage.Measure(parent.Width, parent.Height).Request.Height;
            //relativeLayout.Children.Add(nodateImage,
            //    Constraint.RelativeToParent(parent => parent.Width / 2 - getnodateImageWidth(parent) / 4),
            //    Constraint.RelativeToParent(parent => parent.Height / 2 - getnodateImageHeight(parent) / 4),
            //    Constraint.RelativeToParent((parent) => getnodateImageWidth(parent) / 2),
            //    Constraint.RelativeToParent((parent) => getnodateImageHeight(parent) / 2));

            //double getnonotesLabelWidth(RelativeLayout parent) => nonotesLabel.Measure(parent.Width, parent.Height).Request.Width;
            //relativeLayout.Children.Add(nonotesLabel,
            //    Constraint.RelativeToParent(parent => parent.Width / 2 - getnonotesLabelWidth(parent) / 2),
            //    Constraint.RelativeToParent(parent => parent.Height * .45));

            double newnoteSLWidth(RelativeLayout parent) => NewNotePage_UWP.newnoteSL.Measure(parent.Width, parent.Height).Request.Width;
            relativeLayout.Children.Add(NewNotePage_UWP.newnoteSL,
                Constraint.RelativeToParent(parent => parent.Width - newnoteSLWidth(parent)),
                Constraint.RelativeToParent(parent => todayNotesListView.Y-16.5)
            );

            StackLayout rightSL = new StackLayout
            {
                Children =
                {
                    //верхняя синяя полоса
                    new StackLayout
                    {
                        Children=
                        {
                            righttitle,
                            addImage
                        },
                        HeightRequest=47.5,
                        BackgroundColor=Color.FromHex("1564c0"),
                        Orientation=StackOrientation.Horizontal
                    },
                    relativeLayout
                },
                BackgroundColor = Color.White
            };

            Grid grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto/*Width = new GridLength(0.15, GridUnitType.Star) */},
                    new ColumnDefinition { Width = GridLength.Auto/*Width = new GridLength(0.75, GridUnitType.Star)*/ }
                },
                ColumnSpacing = 0
            };
            grid.Children.Add(leftSL, 0, 0);
            grid.Children.Add(rightSL, 1, 0);
            //relativeLayout.GestureRecognizers.Add(new TapGestureRecognizer
            //{
            //    Command = new Command(() =>
            //    {
            //        if (tap==true)
            //        { addImage.Source = "images/plus.png";
            //            NewNotePage_UWP.newnoteSL.IsVisible = false;
            //        }
            //    })
            //});
            Content = grid;
        }

        public static class ScreenDimensions
        {
            public static double Height { get; set; }
        }
        

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            ScreenDimensions.Height = height;
            
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            OnAppearing();
        }

        protected override async void OnAppearing()
        {
            await App.Database.Update();

            if (page == 1)
            {
                notesListView.IsVisible = false;
                //получаем заметки на сегодня и просроченные
                todayNotes = await App.Database.TodayNotesAsync();
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
                    
                    nonotesLabel.IsVisible = false;
                    nodateSL.IsVisible = false;

                    todayNotesListView.IsVisible = true;
                }
                else
                {
                    nonotesLabel.IsVisible = true;
                    nodateSL.IsVisible = true;

                    todayNotesListView.IsVisible = false;
                }
            }
            else
            {
                todayNotesListView.IsVisible = false;
                //получаем все заметки 
                allnotes = await App.Database.GetItemsAsync();
                if (allnotes.Count != 0)
                {
                    //сортировка
                    var sorted = allnotes.OrderBy(u => u.Date);
                    //получаем группы 
                    groups = sorted.GroupBy(p => p.Date_str).Select(g => new Grouping<string, NoteVM>(g.Key, g));
                    //передаем группы в NoteGroups
                    NoteGrouping = new ObservableCollection<Grouping<string, NoteVM>>(groups);
                    //передаем отсортированный и сгруппированный список
                    notesListView.ItemsSource = NoteGrouping;
                    
                    nonotesLabel.IsVisible = false;
                    nodateSL.IsVisible = false;

                    notesListView.IsVisible = true;
                }
                else
                {
                    notesListView.IsVisible = false;

                    nonotesLabel.IsVisible = true;
                    nodateSL.IsVisible = true;
                }
            }

            countImage.Source = "images/" + NotesViewModel.CountNotes();
            Settings_UWP.todaynotesLabel.Text = "Сегодня выполнено задач: " + App.todaydonenotes;
            Settings_UWP.doneLabel.Text = "Выполнено за все время: " + App.alldonenotes;
            Settings_UWP.expiredLabel.Text = "Просрочено: " + await App.Database.CountExpiredItems();
            Settings_UWP.allnotesLabel.Text = "Запланировано: " + await App.Database.CountItems();
            base.OnAppearing();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //tap = false;
            todayNotesListView.SelectedItem = null;
            notesListView.SelectedItem = null;

            var note = e.Item as NoteVM;
            var vm = BindingContext as NotesViewModel;

            //NewNotePage_UWP.str = null;
            //NewNotePage_UWP.str_old = null;
            //NewNotePage_UWP.count_subtasks = 0;

            NewNotePage_UWP.EditNote(note);

            //OnAppearing();
            //Thread.Sleep(2000);
            //tap = true;
        }

        private void Remove_Clicked(object sender, EventArgs e)
        {
            addImage.Source = "images/plus.png";
            NewNotePage_UWP.newnoteSL.IsVisible = false;
            var button = sender as Button;
            button.Text = "\uE73E";
            var note = button?.BindingContext as NoteVM;
            var vm = BindingContext as NotesViewModel;
            vm?.RemoveCommand.Execute(note);
            OnAppearing();
        }

    }
}