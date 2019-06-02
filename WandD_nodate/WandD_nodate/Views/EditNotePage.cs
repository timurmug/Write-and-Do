using Xamarin.Forms;
using System;
using WandD_nodate.ViewModels;
using WandD_nodate.CustomElements;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WandD_nodate.Views
{
    public partial class EditNotePage : ContentPage
    {
        public NoteVM ViewModel { get; set; }
        string name_old;
        string comment_old;
        DateTime date_old;
        string color_old;
        bool everyday_old;

        Button addButton;
        Button backButton;
        CustomEditor2 nameEntry;
        CustomEditor2 commentEditor;
        CustomDatePicker datePicker;
        Switch repeatSwitch;
        Label datelabel;
        StackLayout choosedaysSL;

        Button mondayButton;
        Button tuesdayButton;
        Button wednesdayButton;
        Button thursdayButton;
        Button fridayButton;
        Button saturdayButton;
        Button sundayButton;
        bool clean_choosebuttons = true;

        int count_subtasks = 0;
        StackLayout subtasksSL;

        int lastButton_int = 0;
        StackLayout chooseColorSL;

        public EditNotePage(NoteVM vm)
        {
            NavigationPage.SetHasBackButton(this, false);
            Title = "Информация о заметке";
            name_old = vm.Name;
            comment_old = vm.Comment;
            date_old = vm.Date;
            color_old = vm.ColorMarker_string;
            everyday_old = vm.Repeat;

            ViewModel = vm;
            BindingContext = ViewModel;

            nameEntry = new CustomEditor2
            {
                Placeholder = "Введите название",
                TextColor = Color.FromHex("0D47A1"),
                //PlaceholderColor = Color.DarkGray,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Entry)),
                Margin = new Thickness(0, 10, 0, 0),
            };
            nameEntry.TextChanged += NameEntry_TextChanged;
            nameEntry.SetBinding(Editor.TextProperty, "Name");

            //////комментарий
            commentEditor = new CustomEditor2
            {
                TextColor = Color.FromHex("0D47A1"),
                Placeholder = "Добавьте комментарий",
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
                Margin = new Thickness(0, 20, 0, 0)
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
            
            /////////////подзадачи
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
                //Children = { subdefaultSL },
                Margin = new Thickness(25, 11, 0, 0)
            };

            string str = ViewModel.Subtasks_string;

            if (String.IsNullOrEmpty(str)==false)
            {
                string[] subtasks_array = str.Split(new char[] { '✖' });
                foreach (char s in str)
                {
                    if (s == '✖')
                        count_subtasks++;
                }

                for (int i = 1; i <= count_subtasks; i++)
                {
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
                        WidthRequest = 240,
                        Text = subtasks_array[i - 1],
                    };

                    if (Addition.IsStrikethrough(newsubtaskEditor.Text) == true)
                        donesubtaskButton.Text = "✔";

                    Label removesubtaskLabel = new Label
                    {
                        Text = "✖",
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(-6, 2, 0, 0),
                        TextColor = Color.Gray,
                        WidthRequest = 35,
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
                        //var element = sender as CustomEditor2;
                        //var str3 = Addition.ConvertToStrikethrough(e.NewTextValue);
                        if (String.IsNullOrWhiteSpace(newsubtaskEditor.Text) == false)
                        {
                            newsubtaskEditor.Text = newsubtaskEditor.Text.Substring(0, 1).ToUpper() + newsubtaskEditor.Text.Remove(0, 1);
                            //if (donesubtaskButton.Text == "✔")
                            //    newsubtaskEditor.Text = str3;
                            //newsubtaskEditor.Text = Addition.ConvertToStrikethrough(newsubtaskEditor.Text);
                        }
                        else
                        {
                            donesubtaskButton.Text = "";
                            newsubtaskEditor.Text = newsubtaskEditor.Text.TrimStart();
                        }
                    };
                    
                    if (subtasksSL.Children.Count > 1)
                        subtasksSL.Children.Remove(subdefaultSL);

                    subtasksSL.Children.Add(newsubtaskSL);
                    subtasksSL.Children.Add(subdefaultSL);

                    string str2 = subtasks_array[i - 1];
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
                }
            }
            else
                subtasksSL.Children.Add(subdefaultSL);

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
                        BorderRadius = 11,
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
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(-6, 2, 0, 0),
                        TextColor = Color.Gray,
                        WidthRequest = 35,
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

            /////////дата
            Image dateImage = new Image
            {
                Source = "calendar.png",
                WidthRequest = 17.5,
                HeightRequest = 17.5,
                Margin = new Thickness(2, 0, 0, 0)
            };
            datePicker = new CustomDatePicker
            {
                //Format = "Сегодня",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(CustomDatePicker)),
                //MinimumDate = DateTime.Today,
                WidthRequest = 163,
                HeightRequest = 35,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(1, 0, 0, 0)
            };
            datePicker.DateSelected += DatePicker_DateSelected;
            //datePicker.SetBinding(DatePicker.DateProperty, "Date");

            if (ViewModel.Date < DateTime.Today)
            {
                datePicker.Format = vm.Date.ToString("d MMMM");
                datePicker.SetBinding(DatePicker.DateProperty, "Date");
                datePicker.MinimumDate = vm.Date;
                datePicker.Date = vm.Date;
            }
            else
            {
                datePicker.Format = "Сегодня";
                datePicker.SetBinding(DatePicker.DateProperty, "Date");
                datePicker.MinimumDate = DateTime.Today;
            }

            StackLayout dateStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(0, 11, 0, 0),
                Children = { dateImage, datePicker }
            };

            ///////////////
            //Label markerlabel = new Label
            //{
            //    Margin = new Thickness(0, 11, 0, 0),
            //    Text = "Пометить?",
            //    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            //};
            Button colorButton0 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("ffffff") }; colorButton0.Clicked += ColorButton_Clicked;
            Button colorButton1 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("ff897d") }; colorButton1.Clicked += ColorButton_Clicked;
            Button colorButton2 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("ffd27a") }; colorButton2.Clicked += ColorButton_Clicked;
            Button colorButton3 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("cbff8a") }; colorButton3.Clicked += ColorButton_Clicked;
            Button colorButton4 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("80afff") }; colorButton4.Clicked += ColorButton_Clicked;
            Button colorButton5 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("b484ff") }; colorButton5.Clicked += ColorButton_Clicked;
            Button colorButton6 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("f9bad0") }; colorButton6.Clicked += ColorButton_Clicked;
            Button colorButton7 = new Button { Style = Addition.buttonStyle2, BackgroundColor = Color.FromHex("cfd8dc") }; colorButton7.Clicked += ColorButton_Clicked;
            chooseColorSL = new StackLayout
            {
                Children = { colorButton0, colorButton1, colorButton2, colorButton3, colorButton4, colorButton5, colorButton6, colorButton7 },
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 20, 0, 0),
            };
            foreach (Button btn in chooseColorSL.Children)
            {
                if (Addition.HexConverter(btn.BackgroundColor) == ViewModel.ColorMarker_string)
                {
                    btn.Text = "✔";
                    lastButton_int = btn.GetHashCode();
                }
            }
            if (lastButton_int == 0)
            {
                colorButton0.Text = "✔";
                lastButton_int = colorButton0.GetHashCode();
            }

            ///////////////////////////////////////////
            Label repeatlabel = new Label
            {
                Text = "Каждый день",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                //HorizontalOptions = LayoutOptions.EndAndExpand
            };
            repeatSwitch = new Switch(); /*{ HorizontalOptions = LayoutOptions.EndAndExpand };*/
            repeatSwitch.SetBinding(Switch.IsToggledProperty, "Repeat");
            repeatSwitch.Toggled += RepeatSwitch_Toggled;
            StackLayout repeatSL = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(0, 11, 0, 0),
                Children = { repeatlabel, repeatSwitch }
            };

            mondayButton = new Button { Text = "П", Style = Addition.buttonStyle }; mondayButton.Clicked += Button_Clicked;
            tuesdayButton = new Button { Text = "В", Style = Addition.buttonStyle }; tuesdayButton.Clicked += Button_Clicked;
            wednesdayButton = new Button { Text = "С", Style = Addition.buttonStyle }; wednesdayButton.Clicked += Button_Clicked;
            thursdayButton = new Button { Text = "Ч", Style = Addition.buttonStyle }; thursdayButton.Clicked += Button_Clicked;
            fridayButton = new Button { Text = "П", Style = Addition.buttonStyle }; fridayButton.Clicked += Button_Clicked;
            saturdayButton = new Button { Text = "С", Style = Addition.buttonStyle }; saturdayButton.Clicked += Button_Clicked;
            sundayButton = new Button { Text = "В", Style = Addition.buttonStyle }; sundayButton.Clicked += Button_Clicked;
            mondayButton.BackgroundColor = Addition.choosedaysColor(1,ViewModel.everyMonday); mondayButton.TextColor = Addition.choosedaysColor(2,ViewModel.everyMonday);
            tuesdayButton.BackgroundColor = Addition.choosedaysColor(1,ViewModel.everyTuesday); tuesdayButton.TextColor = Addition.choosedaysColor(2,ViewModel.everyTuesday);
            wednesdayButton.BackgroundColor = Addition.choosedaysColor(1,ViewModel.everyWednesday); wednesdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyWednesday);
            thursdayButton.BackgroundColor = Addition.choosedaysColor(1,ViewModel.everyThursday); thursdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyThursday);
            fridayButton.BackgroundColor = Addition.choosedaysColor(1,ViewModel.everyFriday); fridayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyFriday);
            saturdayButton.BackgroundColor = Addition.choosedaysColor(1,ViewModel.everySaturday); saturdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everySaturday);
            sundayButton.BackgroundColor = Addition.choosedaysColor(1,ViewModel.everySunday); sundayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everySunday);

            choosedaysSL = new StackLayout
            {
                Children = { mondayButton, tuesdayButton, wednesdayButton, thursdayButton, fridayButton, saturdayButton, sundayButton },
                IsVisible = false,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 5, 0, 0),
            };
            choosedaysSL.SetBinding(StackLayout.IsVisibleProperty, "Repeat");
            
            ///////////////////////////
                addButton = new Button
            {
                Text = "✔",
                BackgroundColor = Color.White,
                TextColor = Color.FromHex("0D47A1")
            };
            addButton.Clicked += AddButton_Clicked;

            Button removeButton = new Button
            {
                Text = "✖",
                BackgroundColor = Color.White,
                TextColor = Color.FromHex("0D47A1")
            };
            removeButton.Clicked += RemoveButton_Clicked;

            backButton = new Button
            {
                Text = "➥",
                BackgroundColor = Color.White,
                TextColor = Color.FromHex("0D47A1")
            };
            backButton.Clicked += BackButton_Clicked;

            StackLayout buttonsStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Children = { addButton, removeButton, backButton }
            };

            //Label0 = new Label { Text = "switch:" + ViewModel.Repeat };
            //Label1 = new Label { Text = "Пон:" + ViewModel.everyMonday };
            //Label2 = new Label { Text = "Вт:" + ViewModel.everyTuesday };
            //Label3 = new Label { Text = "Ср:" + ViewModel.everyWednesday };
            //Label4 = new Label { Text = "Чет:" + ViewModel.everyThursday };
            //Label5 = new Label { Text = "Пят:" + ViewModel.everyFriday };
            //Label6 = new Label { Text = "Суб:" + ViewModel.everySaturday };
            //Label7 = new Label { Text = "Воскр:" + ViewModel.everySunday };

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
                   /* markerlabel,*/chooseColorSL,
                        //Label0,
                        //Label1,
                        //Label2,Label3,Label4,Label5,Label6,Label7,
                    buttonsStackLayout
                },

                    BackgroundColor = Color.White,
                    Padding = new Thickness(15)
                }
            };
            Content = scrollView;

            if (repeatSwitch.IsToggled == false)
                step++;
        }

        private void ColorButton_Clicked(object sender, EventArgs e)
        {
            foreach (Button a in chooseColorSL.Children)
            {
                if (a.GetHashCode() == lastButton_int)
                    a.Text = "";
            }
            var button = sender as Button;
            button.Text = "✔";
            lastButton_int = button.GetHashCode();
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

        int step = 0;
        private void RepeatSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            //if (step > 0)
            //{
            //    if (e.Value == true)
            //    {
            //        ViewModel.everyMonday = true;
            //        ViewModel.everyTuesday = true;
            //        ViewModel.everyWednesday = true;
            //        ViewModel.everyThursday = true;
            //        ViewModel.everyFriday = true;
            //        ViewModel.everySaturday = true;
            //        ViewModel.everySunday = true;
            //        mondayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyMonday); mondayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyMonday);
            //        tuesdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyTuesday); tuesdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyTuesday);
            //        wednesdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyWednesday); wednesdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyWednesday);
            //        thursdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyThursday); thursdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyThursday);
            //        fridayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyFriday); fridayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyFriday);
            //        saturdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everySaturday); saturdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everySaturday);
            //        sundayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everySunday); sundayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everySunday);
            //    }
            //    else
            //    {
            //        ViewModel.everyMonday = false;
            //        ViewModel.everyTuesday = false;
            //        ViewModel.everyWednesday = false;
            //        ViewModel.everyThursday = false;
            //        ViewModel.everyFriday = false;
            //        ViewModel.everySaturday = false;
            //        ViewModel.everySunday = false;
            //        mondayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyMonday); mondayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyMonday);
            //        tuesdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyTuesday); tuesdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyTuesday);
            //        wednesdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyWednesday); wednesdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyWednesday);
            //        thursdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyThursday); thursdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyThursday);
            //        fridayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everyFriday); fridayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everyFriday);
            //        saturdayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everySaturday); saturdayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everySaturday);
            //        sundayButton.BackgroundColor = Addition.choosedaysColor(1, ViewModel.everySunday); sundayButton.TextColor = Addition.choosedaysColor(2, ViewModel.everySunday);
            //    }
            //    step++;
            //    Label0.Text = "switch: " + e.Value;
            //    Label1.Text = "Пон:" + ViewModel.everyMonday;
            //    Label2.Text = "Вт:" + ViewModel.everyTuesday;
            //    Label3.Text = "Ср:" + ViewModel.everyWednesday;
            //    Label4.Text = "Чет:" + ViewModel.everyThursday;
            //    Label5.Text = "Пят:" + ViewModel.everyFriday;
            //    Label6.Text = "Суб:" + ViewModel.everySaturday;
            //    Label7.Text = "Воскр:" + ViewModel.everySunday;
            //}
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //clean_choosebuttons = false;
            //var button = sender as Button;
            //bool a = false;
            //switch (choosedaysSL.Children.IndexOf(button))
            //{
            //    case 0: ViewModel.everyMonday = !ViewModel.everyMonday; a = ViewModel.everyMonday; Label1.Text = "Пон:" + ViewModel.everyMonday; break;
            //    case 1: ViewModel.everyTuesday = !ViewModel.everyTuesday; a = ViewModel.everyTuesday; Label2.Text = "Вт:" + ViewModel.everyTuesday; break;
            //    case 2: ViewModel.everyWednesday = !ViewModel.everyWednesday; a = ViewModel.everyWednesday; Label3.Text = "Ср:" + ViewModel.everyWednesday; break;
            //    case 3: ViewModel.everyThursday = !ViewModel.everyThursday; a = ViewModel.everyThursday; Label4.Text = "Чет:" + ViewModel.everyThursday; break;
            //    case 4: ViewModel.everyFriday = !ViewModel.everyFriday; a = ViewModel.everyFriday; Label5.Text = "Пят:" + ViewModel.everyFriday; break;
            //    case 5: ViewModel.everySaturday = !ViewModel.everySaturday; a = ViewModel.everySaturday; Label6.Text = "Суб:" + ViewModel.everySaturday; break;
            //    case 6: ViewModel.everySunday = !ViewModel.everySunday; a = ViewModel.everySunday; Label7.Text = "Воскр:" + ViewModel.everySunday; break;
            //    default: break;
            //};
            //button.BackgroundColor = Addition.choosedaysColor(1, a);
            //button.TextColor = Addition.choosedaysColor(2, a);

            //if ((ViewModel.everyMonday == false) && (ViewModel.everyTuesday == false) && (ViewModel.everyWednesday == false)
            //   && (ViewModel.everyThursday == false) && (ViewModel.everyFriday == false)
            //   && (ViewModel.everySaturday == false) && (ViewModel.everySunday == false))
            //    ViewModel.Repeat = false;
            //if (button.BackgroundColor == Color.LightGray)
            //{
            //    button.BackgroundColor = Color.FromHex("E3F2FD");
            //    button.TextColor = Color.FromHex("0D47A1");
            //}
            //else
            //{
            //    //button.BackgroundColor = Color.FromHex("0D47A1");
            //    button.BackgroundColor = Color.LightGray;
            //    button.TextColor = Color.White;
            //}
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            nameEntry.Text = nameEntry.Text.TrimEnd();
            if (String.IsNullOrEmpty(commentEditor.Text) == false)
                commentEditor.Text = commentEditor.Text.Trim();
            string str = null;
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

            if (ViewModel.Date >= DateTime.Today)
                ViewModel.IsOverdue = false;
            else
                ViewModel.Overdue_str = "(от " + ViewModel.Date.ToString("dd MMMM")+ ")";
            
            await App.Database.SaveItemAsync(ViewModel);
            await Navigation.PopAsync();
        }

        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            if (ViewModel != null)
            {
                await App.Database.DeleteItemAsync(ViewModel);
            }
            await Navigation.PopAsync();
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (DateTime.Today.Year < e.NewDate.Year)
            {
                datePicker.Format = "d MMMM yyyy";
            }
            else
            {
                if (e.NewDate == DateTime.Today.AddDays(+1))
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
            if (String.IsNullOrWhiteSpace(nameEntry.Text) == false)
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
            }
            else
            {
                addButton.IsEnabled = false;
                nameEntry.Text = nameEntry.Text.TrimStart();
            }
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
            if (String.IsNullOrWhiteSpace(element.Text) == false)
            {
                element.Text = element.Text.Substring(0, 1).ToUpper() + element.Text.Remove(0, 1);
            }
            else { element.Text = element.Text.TrimStart(); }
        }

        private async void BackButton_Clicked(object sender, System.EventArgs e)
        {
            nameEntry.Text = name_old;
            commentEditor.Text = comment_old;
            datePicker.Date = date_old;
            ViewModel.ColorMarker_string = color_old;
            repeatSwitch.IsToggled = everyday_old;
            await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //if (NotesViewModel.oldNote != null)
            //{
            //    NotesViewModel.oldNote.IsVisible = false;
            //    NotesViewModel.UpdateNotes(NotesViewModel.oldNote);
            //}
            
        }

        private string ConvertToStrikethrough(string stringToChange)
        {
            var newString = "";
            foreach (var character in stringToChange)
            {
                newString += $"{character}\u0336";
            }

            return newString;
        }

        private string ConvertFromStrikethrough(string stringToChange)
        {
            return Regex.Replace(stringToChange, @"[^\u0000-\u007F]+", stringToChange);
        }
    }
}
