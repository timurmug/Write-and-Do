using Xamarin.Forms;
using System;
using WandD_nodate.CustomElements;
using WandD_nodate.ViewModels;

namespace WandD_nodate.Views
{
    public partial class NewNotePage : ContentPage
    {
        public NoteVM ViewModel { get; private set; }
        Button addButton;
        CustomEditor2 nameEntry;
        CustomEditor2 commentEditor;
        CustomDatePicker datePicker;
        StackLayout choosedaysSL;
        Switch repeatSwitch;

        Button mondayButton;
        Button tuesdayButton;
        Button wednesdayButton;
        Button thursdayButton;
        Button fridayButton;
        Button saturdayButton;
        Button sundayButton;
        
        int count_subtasks;
        StackLayout subtasksSL;

        int lastButton_int = 0;
        StackLayout chooseColorSL;

        public NewNotePage(NoteVM vm)
        {
            NavigationPage.SetHasBackButton(this, false);
            Title = "Новая заметка";
            ViewModel = vm;
            BindingContext = ViewModel;
            count_subtasks = 0;

            nameEntry = new CustomEditor2
            {
                Placeholder = "Введите название",
                TextColor = Color.FromHex("0D47A1"),
                //PlaceholderColor = Color.DarkGray,
                FontSize=Device.GetNamedSize(NamedSize.Large, typeof(Entry)),
                Margin = new Thickness(0, 10, 0, 0),
            };
            nameEntry.SetBinding(Editor.TextProperty, "Name");
            nameEntry.TextChanged += NameEntry_TextChanged;

            //////комментарий
            commentEditor = new CustomEditor2
            {
                TextColor = Color.FromHex("0D47A1"),
                Placeholder="Добавьте комментарий",
            };
            commentEditor.TextChanged += Editor_TextChanged;
            commentEditor.SetBinding(CustomEditor2.TextProperty, "Comment");
            Grid commentGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto },
                },
                Margin=new Thickness(0, 20, 0,0)
            };
            commentGrid.Children.Add(new Label
            {
                Text = "//",
                WidthRequest = 20,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalTextAlignment = TextAlignment.Center
            }, 0, 0);
            commentGrid.Children.Add(commentEditor, 1, 0);

            /////////////////////подзадачи
            Label subtaskLabel = new Label
            {
                Text = "Добавить подзадачу",
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(2, 0, 0, 0)
            };
            StackLayout subdefaultSL = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text="➥",
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        HorizontalOptions=LayoutOptions.Center,
                        Margin = new Thickness(3, 0, 0, 0)
                    },
                    subtaskLabel
                },
                Orientation = StackOrientation.Horizontal
            };
            subtasksSL = new StackLayout
            {
                Children ={ subdefaultSL },
                Margin=new Thickness(25, 11, 0,0)
            };
            
            subtaskLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    count_subtasks++;

                    CustomButton donesubtaskButton = new CustomButton
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                        WidthRequest = 23,
                        HeightRequest = 23,
                        BorderRadius = 12,
                        BackgroundColor = Color.White,
                        BorderWidth = 1,
                        BorderColor = Color.Gray,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 3, 0, 0)
                    };
                    CustomEditor2 newsubtaskEditor = new CustomEditor2
                    {
                        Placeholder = "Введите название",
                        TextColor = Color.FromHex("0D47A1"),
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry)),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        WidthRequest = 240
                    };
                    Label removesubtaskLabel = new Label
                    {
                        Text = "✖",
                        HorizontalTextAlignment=TextAlignment.Center,
                        VerticalTextAlignment=TextAlignment.Center,
                        BackgroundColor = Color.Transparent,
                        Margin=new Thickness(-6,2,0,0),
                        TextColor = Color.Gray,
                        WidthRequest=35,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Start,
                    };
                    StackLayout newsubtaskSL = new StackLayout
                    {
                        Children = { donesubtaskButton, newsubtaskEditor, removesubtaskLabel },
                        Orientation = StackOrientation.Horizontal
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

                    if (subtasksSL.Children[0] == subdefaultSL)
                        subtasksSL.Children.Remove(subdefaultSL);
                    if (subtasksSL.Children.Count > 1)
                        subtasksSL.Children.Remove(subdefaultSL);

                    subtasksSL.Children.Add(newsubtaskSL);
                    subtasksSL.Children.Add(subdefaultSL);

                    donesubtaskButton.Clicked += (sender, e) => 
                    {
                        if (String.IsNullOrWhiteSpace(newsubtaskEditor.Text) == false)
                        {
                            if (Addition.IsStrikethrough(newsubtaskEditor.Text) == true)
                            {
                                donesubtaskButton.Text = "";
                                newsubtaskEditor.Text = Addition.ConvertFromStrikethrough(newsubtaskEditor.Text);
                            }
                            else
                            {
                                donesubtaskButton.Text = "✔";
                                newsubtaskEditor.Text = Addition.ConvertToStrikethrough(newsubtaskEditor.Text);
                            }
                        }
                    };
                    removesubtaskLabel.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() => { count_subtasks--; subtasksSL.Children.Remove(newsubtaskSL); })
                    });
                })
            });

            ////////////дата
            Image dateImage = new Image
            {
                Source = "calendar.png",
                WidthRequest=17.5,
                HeightRequest= 17.5,
                Margin=new Thickness(2,0,0,0)
            };
            //datePicker = new CustomDatePicker
            //{
            //    Format = "Сегодня",
            //    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(CustomDatePicker)),
            //    MinimumDate = DateTime.Today,
            //    WidthRequest = 165,
            //    HeightRequest = 35,
            //    HorizontalOptions=LayoutOptions.Start,
            //    Margin = new Thickness(1, 0, 0, 0)
            //};
            //datePicker.DateSelected += DatePicker_DateSelected;
            //datePicker.SetBinding(CustomDatePicker.DateProperty, "Date");
             datePicker = new CustomDatePicker
            {
                Format = "Сегодня",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(CustomDatePicker)),
                MinimumDate = DateTime.Today,
                WidthRequest = 165,
                HeightRequest = 35,
                HorizontalOptions=LayoutOptions.Start,
                Margin = new Thickness(1, 0, 0, 0)
            };
            datePicker.DateSelected += DatePicker_DateSelected;
            datePicker.SetBinding(CustomDatePicker.DateProperty, "Date");

            StackLayout dateStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(0, 11, 0, 0),
                Children = { dateImage, datePicker}
            };

            ///////////////
            //Label markerlabel = new Label
            //{
            //    Margin = new Thickness(0, 11, 0, 0),
            //    Text = "Пометить?",
            //    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            //};
            Button colorButton0 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("ffffff") , Text = "✔" }; colorButton0.Clicked += ColorButton_Clicked;
            lastButton_int = colorButton0.GetHashCode();
            Button colorButton1 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("ff897d") }; colorButton1.Clicked += ColorButton_Clicked;
            Button colorButton2 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("ffd27a") }; colorButton2.Clicked += ColorButton_Clicked;
            Button colorButton3 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("cbff8a") }; colorButton3.Clicked += ColorButton_Clicked;
            Button colorButton4 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("80afff") }; colorButton4.Clicked += ColorButton_Clicked;
            Button colorButton5 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("b484ff") }; colorButton5.Clicked += ColorButton_Clicked;
            Button colorButton6 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("f9bad0") }; colorButton6.Clicked += ColorButton_Clicked;
            Button colorButton7 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("cfd8dc") }; colorButton7.Clicked += ColorButton_Clicked;
            chooseColorSL = new StackLayout
            {
                Children = { colorButton0,colorButton1, colorButton2, colorButton3, colorButton4, colorButton5, colorButton6, colorButton7 },
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 20, 0, 0),
            };

            ///////////////////////////////////////////
            Label repeatlabel = new Label
            {
                Text = "Каждый день",
                //Margin = new Thickness(0, 3, 0, 0),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                //HorizontalOptions = LayoutOptions.End
            };
            repeatSwitch = new Switch(); /*{ HorizontalOptions=LayoutOptions.EndAndExpand};*/
            repeatSwitch.SetBinding(Switch.IsToggledProperty, "Repeat");
            //repeatSwitch.Toggled += RepeatSwitch_Toggled;
            StackLayout repeatSL = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(0, 11, 0, 0),
                Children = { repeatlabel, repeatSwitch }
            };

            mondayButton = new Button {Text = "П", Style = Addition.buttonStyle }; mondayButton.Clicked += Button_Clicked; 
            tuesdayButton = new Button {Text = "В",Style = Addition.buttonStyle }; tuesdayButton.Clicked += Button_Clicked; 
            wednesdayButton = new Button { Text = "С", Style = Addition.buttonStyle }; wednesdayButton.Clicked += Button_Clicked; 
            thursdayButton = new Button { Text = "Ч", Style = Addition.buttonStyle }; thursdayButton.Clicked += Button_Clicked;
            fridayButton = new Button { Text = "П", Style = Addition.buttonStyle }; fridayButton.Clicked += Button_Clicked;
            saturdayButton = new Button { Text = "С", Style = Addition.buttonStyle }; saturdayButton.Clicked += Button_Clicked;
            sundayButton = new Button { Text = "В", Style = Addition.buttonStyle }; sundayButton.Clicked += Button_Clicked;

            choosedaysSL = new StackLayout
            {
                Children = { mondayButton, tuesdayButton, wednesdayButton, thursdayButton , fridayButton , saturdayButton , sundayButton },
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions=LayoutOptions.Center,
                Margin = new Thickness(0, 5, 0, 0),
            };
            choosedaysSL.SetBinding(IsVisibleProperty, "Repeat");

            /////////////////////////////////
            addButton = new Button
            {
                Text = "✔",
                BackgroundColor = Color.White,
                TextColor = Color.Gray,
                IsEnabled = false
            };
            addButton.Pressed += AddButton_Clicked;
            //addButton.Clicked += AddButton_Clicked;

            Button backButton = new Button
            {
                Text = "➥",
                BackgroundColor = Color.White,
                TextColor = Color.FromHex("0D47A1")
            };
            backButton.SetBinding(Button.CommandProperty, "ListViewModel.BackCommand");
            //backButton.Clicked += RemoveSelectedItem;

            StackLayout buttonsStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Children = { addButton,  backButton }
            };

            //Label0 = new Label { Text = "switch:" + ViewModel.Repeat };
            //Label1 = new Label { Text = "Пон:" + ViewModel.everyMonday };
            //Label2 = new Label { Text = "Вт:" + ViewModel.everyTuesday };
            //Label3 = new Label { Text = "Ср:" + ViewModel.everyWednesday };
            //Label4 = new Label { Text = "Чет:" + ViewModel.everyThursday };
            //Label5 = new Label { Text = "Пят:" + ViewModel.everyFriday };
            //Label6 = new Label { Text = "Суб:" + ViewModel.everySaturday };
            //Label7 = new Label { Text = "Воскр:" + ViewModel.everySunday };

            Label0 = new Label();
            Label1 = new Label();
            Button button = new Button { Text = "check" };
            button.Clicked += (sender, e) => 
            {
                string str = null;
                int count = 0;
                for (int i = 0; i <= count_subtasks - 1; i++)
                {
                    if (String.IsNullOrEmpty(((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text) == false)
                    {
                        str += ((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text + "✖";
                        count++;
                    }
                }
                Label0.Text =str;
                Label1.Text= count.ToString();
            };

            ScrollView scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                {
                    nameEntry,
                    commentGrid,
                    subtasksSL,
                    dateStackLayout,
                    repeatSL, /*choosedaysSL,*/
                    /*markerlabel,*/chooseColorSL,
                        //Label0,
                        //Label1,
                        //button,
                        //Label2,Label3,Label4,Label5,Label6,Label7,
                    buttonsStackLayout
                },

                    BackgroundColor = Color.White,
                    Padding = new Thickness(15)
                }
            };
            Content = scrollView;
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            nameEntry.Text = nameEntry.Text.TrimEnd();
            if (String.IsNullOrEmpty(commentEditor.Text)==false)
                commentEditor.Text = commentEditor.Text.Trim();
            string str=null;
            for (int i = 0; i <= count_subtasks - 1; i++)
            {
                if (String.IsNullOrWhiteSpace(((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text) == false)
                {
                    if (((subtasksSL.Children[i] as StackLayout).Children[0] as CustomButton).Text == "✔")
                        ((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text = Addition.ConvertToStrikethrough(((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text);

                    ((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text = ((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text.Trim();
                    str += ((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text + "✖";
                }
            }
            ViewModel.Subtasks_string = str;
        }

        //private void subtaskEditor_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var element = sender as Editor;
        //    if (String.IsNullOrWhiteSpace(element.Text) == false)
        //    {
        //        element.Text = element.Text.Substring(0, 1).ToUpper() + element.Text.Remove(0, 1);
        //    }
        //    else { element.Text = element.Text.TrimStart(); }
        //}

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var element = sender as Editor;
            if (String.IsNullOrWhiteSpace(element.Text)==false)
            {
                element.Text = element.Text.Substring(0, 1).ToUpper() + element.Text.Remove(0, 1);
            }
            else { element.Text=element.Text.TrimStart(); }
        }

        private void ColorButton_Clicked(object sender, EventArgs e)
        {
            foreach (Button btn in chooseColorSL.Children)
            {
                if (btn.GetHashCode() == lastButton_int)
                    btn.Text = "";
            }
            var button = sender as Button;
            button.Text = "✔";
            lastButton_int= button.GetHashCode();
            ViewModel.ColorMarker_string = Addition.HexConverter(button.BackgroundColor);
        }
        
        Label Label0;
        Label Label1;
        Label Label2;
        Label Label3;
        Label Label4;
        Label Label5;
        Label Label6;
        Label Label7;

        private void RepeatSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                ViewModel.everyMonday = true;
                ViewModel.everyTuesday = true;
                ViewModel.everyWednesday = true;
                ViewModel.everyThursday = true;
                ViewModel.everyFriday = true;
                ViewModel.everySaturday = true;
                ViewModel.everySunday = true;
                mondayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyMonday); mondayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyMonday);
                tuesdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyTuesday); tuesdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyTuesday);
                wednesdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyWednesday); wednesdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyWednesday);
                thursdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyThursday); thursdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyThursday);
                fridayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyFriday); fridayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyFriday);
                saturdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everySaturday); saturdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everySaturday);
                sundayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everySunday); sundayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everySunday);
            }
            else
            {
                ViewModel.everyMonday = false;
                ViewModel.everyTuesday = false;
                ViewModel.everyWednesday = false;
                ViewModel.everyThursday = false;
                ViewModel.everyFriday = false;
                ViewModel.everySaturday = false;
                ViewModel.everySunday = false;
                mondayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyMonday); mondayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyMonday);
                tuesdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyTuesday); tuesdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyTuesday);
                wednesdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyWednesday); wednesdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyWednesday);
                thursdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyThursday); thursdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyThursday);
                fridayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyFriday); fridayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyFriday);
                saturdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everySaturday); saturdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everySaturday);
                sundayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everySunday); sundayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everySunday);
            }
            Label0.Text = "switch: " + e.Value;
            Label1.Text = "Пон:" + ViewModel.everyMonday;
            Label2.Text = "Вт:" + ViewModel.everyTuesday;
            Label3.Text = "Ср:" + ViewModel.everyWednesday;
            Label4.Text = "Чет:" + ViewModel.everyThursday;
            Label5.Text = "Пят:" + ViewModel.everyFriday;
            Label6.Text = "Суб:" + ViewModel.everySaturday;
            Label7.Text = "Воскр:" + ViewModel.everySunday;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            bool a=false;
            switch (choosedaysSL.Children.IndexOf(button))
            {
                case 0: ViewModel.everyMonday = !ViewModel.everyMonday; a = ViewModel.everyMonday; Label1.Text = "Пон:" + ViewModel.everyMonday; break;
                case 1: ViewModel.everyTuesday = !ViewModel.everyTuesday; a = ViewModel.everyTuesday; Label2.Text = "Вт:" + ViewModel.everyTuesday; break;
                case 2: ViewModel.everyWednesday = !ViewModel.everyWednesday; a = ViewModel.everyWednesday; Label3.Text = "Ср:" + ViewModel.everyWednesday; break;
                case 3: ViewModel.everyThursday = !ViewModel.everyThursday; a = ViewModel.everyThursday; Label4.Text = "Чет:" + ViewModel.everyThursday; break;
                case 4: ViewModel.everyFriday = !ViewModel.everyFriday; a = ViewModel.everyFriday; Label5.Text = "Пят:" + ViewModel.everyFriday; break;
                case 5: ViewModel.everySaturday = !ViewModel.everySaturday; a = ViewModel.everySaturday; Label6.Text = "Суб:" + ViewModel.everySaturday; break;
                case 6: ViewModel.everySunday = !ViewModel.everySunday; a = ViewModel.everySunday; Label7.Text = "Воскр:" + ViewModel.everySunday; break;
                default: break;
            };
            button.BackgroundColor = Addition.choosedaysColor(1, a);
            button.TextColor = Addition.choosedaysColor(2, a);

            if ((ViewModel.everyMonday == false) && (ViewModel.everyTuesday == false) && (ViewModel.everyWednesday == false)
               && (ViewModel.everyThursday == false) && (ViewModel.everyFriday == false)
               && (ViewModel.everySaturday == false) && (ViewModel.everySunday == false))
                ViewModel.Repeat = false;
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (DateTime.Today.Year < e.NewDate.Year)
            {
                datePicker.Format = "d MMMM yyyy";
            }
            else
            {
                if (e.NewDate== DateTime.Today.AddDays(+1))
                    datePicker.Format = "Завтра";
                else if (e.NewDate == DateTime.Today)
                        datePicker.Format = "Сегодня";
                     else if (e.NewDate == DateTime.Today.AddDays(+2))
                            datePicker.Format = "Послезавтра";
                         else
                             datePicker.Format = "d MMMM";
            }
        }

        private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(nameEntry.Text)==false)
            {
                nameEntry.Text = nameEntry.Text.Substring(0, 1).ToUpper() + nameEntry.Text.Remove(0, 1);
                if (nameEntry.Text.Length > 58)
                {
                    string check = nameEntry.Text;
                    check = check.Remove(58);
                    nameEntry.Text = check;
                }
                addButton.TextColor = Color.FromHex("0D47A1");
                addButton.IsEnabled = true;
                addButton.SetBinding(Button.CommandProperty, "ListViewModel.SaveNoteCommand");
                Binding binding = new Binding { Source = ViewModel };
                addButton.SetBinding(Button.CommandParameterProperty, binding);
            }
            else
            {
                addButton.IsEnabled = false;
                nameEntry.Text = nameEntry.Text.TrimStart();
            }
        }
        
    }
}
