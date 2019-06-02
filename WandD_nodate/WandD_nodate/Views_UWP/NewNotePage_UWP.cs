using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WandD_nodate.ViewModels;
using WandD_nodate.CustomElements;
using Xamarin.Forms;
using WandD_nodate.Views_UWP;

namespace WandD_nodate.Views_UWP
{
	public class NewNotePage_UWP : ContentPage
	{
        public static CustomEditor2 nameEntry = new CustomEditor2
        {
            FontFamily = "Open Sans",
            Placeholder = "Введите название",
            FontSize = 25,
            TextColor = Color.FromHex("0D47A1"),
            Margin = new Thickness(15, 10, 10, 0),
            VerticalOptions = LayoutOptions.Start,
            BackgroundColor = Color.Transparent,
            AutoSize = EditorAutoSizeOption.TextChanges,
            WidthRequest = 352
        };
        
        public static CustomEditor2 commentEntry = new CustomEditor2
        {
            FontFamily = "Open Sans",
            TextColor = Color.FromHex("0D47A1"),
            Placeholder = "Добавьте комментарий",
            WidthRequest=290,
            FontSize=20,
            BackgroundColor=Color.Transparent
        };

        /////////////////////подзадачи
        public static int count_subtasks;
        public static Label subtaskLabel = new Label
        {
            Text = "Добавить подзадачу",
            FontFamily = "Open Sans",
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(2, 0, 0, 0),
            //TextColor=nameEntry.PlaceholderColor
        };
        public static StackLayout subdefaultSL = new StackLayout
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
        public static StackLayout subtasksSL = new StackLayout
        {
            Children = { subdefaultSL },
            Margin = new Thickness(25, 11, 0, 0),
            Spacing=0
        };


        ///////
        public static DatePicker datePicker = new DatePicker
        {
            FontFamily = "Open Sans",
            Format = "Сегодня",
            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(DatePicker)),
            Margin = new Thickness(10, 15, 10, 0),
            MinimumDate = DateTime.Today,
            
        };

        public static Button colorButton0 = new Button { Style = Addition.buttonStyle2, FontFamily = "Segoe MDL2 Assets", BackgroundColor = Color.FromHex("ffffff") }; 
        public static Button colorButton1 = new Button { Style = Addition.buttonStyle2, FontFamily = "Segoe MDL2 Assets", BackgroundColor = Color.FromHex("ff897d") }; 
        public static Button colorButton2 = new Button { Style = Addition.buttonStyle2, FontFamily = "Segoe MDL2 Assets", BackgroundColor = Color.FromHex("ffd27a") }; 
        public static Button colorButton3 = new Button { Style = Addition.buttonStyle2, FontFamily = "Segoe MDL2 Assets", BackgroundColor = Color.FromHex("cbff8a") }; 
        public static Button colorButton4 = new Button { Style = Addition.buttonStyle2, FontFamily = "Segoe MDL2 Assets", BackgroundColor = Color.FromHex("80afff") }; 
        public static Button colorButton5 = new Button { Style = Addition.buttonStyle2, FontFamily = "Segoe MDL2 Assets", BackgroundColor = Color.FromHex("b484ff") }; 
        public static Button colorButton6 = new Button { Style = Addition.buttonStyle2, FontFamily = "Segoe MDL2 Assets", BackgroundColor = Color.FromHex("f9bad0") }; 
        public static Button colorButton7 = new Button { Style = Addition.buttonStyle2, FontFamily = "Segoe MDL2 Assets", BackgroundColor = Color.FromHex("cfd8dc") }; 
        public static StackLayout chooseColorSL = new StackLayout
        {
            Children = { colorButton0,colorButton1, colorButton2,
                colorButton3, colorButton4, colorButton5, colorButton6, colorButton7},
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(10, 15, 10, 0),
        };

        public static Label repeatlabel = new Label
        {
            FontFamily = "Open Sans",
            Text = "Каждый день",
            Margin = new Thickness(15, 7, 0, 0),
            FontSize = 17.5
        };
        public static Switch repeatSwitch = new Switch
        {
            WidthRequest = 45,
            Margin=new Thickness(5,0,0,0),
            HorizontalOptions = LayoutOptions.Start
        };
        public static StackLayout repeatSL = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Margin = new Thickness(0, 15, 10, 0),
            Children = { repeatlabel, repeatSwitch }
        };

        public static Button addnewnoteButton = new Button
        {
            FontFamily = "Open Sans",
            Text = "Добавить",
            BackgroundColor = Color.White,
            TextColor = Color.FromHex("0D47A1"),
            IsEnabled = false
        };
        public static Button removeButton = new Button
        {
            FontFamily = "Open Sans",
            Text = "Удалить",
            BackgroundColor = Color.White,
            TextColor = Color.FromHex("0D47A1"),
            IsVisible = false
        };
        public static Button backButton = new Button
        {
            FontFamily = "Open Sans",
            Text = "Отменить",
            BackgroundColor = Color.White,
            TextColor = Color.FromHex("0D47A1")
        };

        public static StackLayout buttonsStackLayout = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.End,
            Children = { addnewnoteButton, removeButton, backButton },
            Margin=new Thickness(0,15,0,12.5)
        };

        public static StackLayout StackLayout = new StackLayout
        {
            Orientation = StackOrientation.Vertical,
            Spacing = 0,
            Children =
                {
                    //new Label{Text=count_subtasks.ToString()},
                    nameEntry,
                    new StackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                Text = "//",
                                WidthRequest = 20,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                VerticalTextAlignment = TextAlignment.Start,
                                HorizontalTextAlignment = TextAlignment.Center,
                                Margin=new Thickness(0,5,0,0)
                            },
                            commentEntry,
                        },
                        Orientation=StackOrientation.Horizontal,
                        Margin = new Thickness(15, 0, 10, 0),
                    },
                    subtasksSL,
                    datePicker,
                    repeatSL,
                    chooseColorSL,
                    buttonsStackLayout
                },
        };

        public static ScrollView newnoteSL = new ScrollView
        {
            BackgroundColor = Color.FromHex("EEEEEE"),
            IsVisible = false,
            Content = StackLayout,
            //HeightRequest = -1
        };

        //public static StackLayout newnoteSL = new StackLayout
        //{
        //    BackgroundColor = Color.FromHex("EEEEEE"),
        //    IsVisible = false,
        //    Children =
        //        {
        //            new Label{Text=count_subtasks.ToString()},
        //            nameEntry,
        //            new StackLayout
        //            {
        //                Children =
        //                {
        //                    new Label
        //                    {
        //                        Text = "//",
        //                        WidthRequest = 20,
        //                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
        //                        VerticalTextAlignment = TextAlignment.Start,
        //                        HorizontalTextAlignment = TextAlignment.Center,
        //                        Margin=new Thickness(0,5,0,0)
        //                    },
        //                    commentEntry,
        //                },
        //                Orientation=StackOrientation.Horizontal,
        //                Margin = new Thickness(15, 15, 10, 0),
        //            },
        //            subtasksSL,
        //            datePicker,
        //            repeatSL,
        //            chooseColorSL,
        //            buttonsStackLayout
        //        },
        //};

        //public static NewNotePage_UWP(NoteVM vm)
        //{
        //}

        static bool edit = false;
        static NoteVM editnote;
        static NoteVM editnote2;
        public static Button additionButton=new Button();
        public static int lastButton_int = 0;

        public static void EditNote(NoteVM vm)
        {
            count_subtasks = 0;
            str = null;
            str_old = null;
            subtasksSL.Children.Clear();
            //subtasksSL.Children.Add(subdefaultSL);

            editnote = vm;
            nameEntry.Text = vm.Name;
            commentEntry.Text = vm.Comment;
            str_old = vm.Subtasks_string;

            if (String.IsNullOrEmpty(str_old) == false)
            {
                string[] subtasks_array = str_old.Split(new char[] { '✖' });
                foreach (char s in str_old)
                {
                    if (s == '✖')
                        count_subtasks++;
                }

                for (int i = 1; i <= count_subtasks; i++)
                {
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
                        AutoSize=EditorAutoSizeOption.TextChanges,
                        WidthRequest = 282,
                        BackgroundColor = Color.Transparent,
                        Text = subtasks_array[i - 1],
                    };

                    //if (Addition.IsStrikethrough(newsubtaskEditor.Text) == true)
                    //    donesubtaskButton.Text = "\uE73E";
                    if (subtasks_array[i - 1].StartsWith("➥") == true)
                    {
                        donesubtaskButton.Text = "\uE73E";
                        newsubtaskEditor.Text = newsubtaskEditor.Text.Remove(0,1);
                    }

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
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        VerticalOptions = LayoutOptions.Start,
                    };
                    StackLayout newsubtaskSL = new StackLayout
                    {
                        Children = { donesubtaskButton, newsubtaskEditor, removesubtaskLabel },
                        Orientation = StackOrientation.Horizontal,
                        Margin=0
                    };
                    newsubtaskEditor.TextChanged += (sender2, e2) =>
                    {
                        if (String.IsNullOrWhiteSpace(newsubtaskEditor.Text) == false)
                        {
                            newsubtaskEditor.Text = newsubtaskEditor.Text.Substring(0, 1).ToUpper() + newsubtaskEditor.Text.Remove(0, 1);
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
                    donesubtaskButton.Clicked += (sender3, e3) =>
                    {
                        if (String.IsNullOrWhiteSpace(newsubtaskEditor.Text) == false)
                        {
                            if (str2.StartsWith("➥") == true)
                            {
                                donesubtaskButton.Text = "";
                                //newsubtaskEditor.Text = newsubtaskEditor.Text.Remove(0, 1);
                                subtasks_array[i - 1] = str2.Remove(0, 1);
                            }
                            else
                            {
                                donesubtaskButton.Text = "\uE73E";
                                //newsubtaskEditor.Text = "➥"+ newsubtaskEditor.Text;
                                subtasks_array[i - 1] = "➥" + str2;
                            }

                            //if (Addition.IsStrikethrough(newsubtaskEditor.Text) == true)
                            //{
                            //    donesubtaskButton.Text = "";
                            //    newsubtaskEditor.Text = Addition.ConvertFromStrikethrough(newsubtaskEditor.Text);
                            //}
                            //else
                            //{
                            //    donesubtaskButton.Text = "\uE73E";
                            //    newsubtaskEditor.Text = Addition.ConvertToStrikethrough(newsubtaskEditor.Text);
                            //}
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

            //(StackLayout.Children[0] as Label).Text = vm.Subtasks_string;
            //(newnoteSL.Children[0] as Label).Text = "сount: "+count_subtasks.ToString() +"\n"+
            //    "str: "+str+"\n"+
            //    "str_old: "+str_old;

            if (editnote.Date >= DateTime.Today)
                datePicker.MinimumDate = DateTime.Today;
            else
                datePicker.MinimumDate = vm.Date;
            datePicker.Date = vm.Date;

            foreach (Button btn in chooseColorSL.Children)
            {
                btn.Text = "";
                lastButton_int = 0;
            }
            foreach (Button btn in chooseColorSL.Children)
            {
                if (Addition.HexConverter(btn.BackgroundColor) == vm.ColorMarker_string)
                {
                    btn.Text = "\uE73E";
                    lastButton_int = btn.GetHashCode();
                    additionButton.BackgroundColor = btn.BackgroundColor;
                }
            }

            repeatSwitch.IsToggled = vm.Repeat;
            newnoteSL.IsVisible = true;
            edit = true;
            removeButton.IsVisible = true;
            addnewnoteButton.Text = "Сохранить";
            MainPage_UWP.addImage.Source = "images/cross.png";
        }

        public static void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //nameEntry.Text = nameEntry.Text.TrimStart();
            if (String.IsNullOrWhiteSpace(nameEntry.Text) == false)
            {
                nameEntry.Text = nameEntry.Text.Substring(0, 1).ToUpper() + nameEntry.Text.Remove(0, 1);
                addnewnoteButton.TextColor = Color.White;
                addnewnoteButton.BackgroundColor = Color.FromHex("0D47A1");
                addnewnoteButton.IsEnabled = true;

                //if (nameEntry.Text.Length > 39)
                //{
                //    string check = nameEntry.Text;
                //    check = check.Remove(39);
                //    nameEntry.Text = check;
                //}
            }
            else
            {
                addnewnoteButton.IsEnabled = false;
                nameEntry.Text = nameEntry.Text.TrimStart();
            }
        }

        public static string str_old;
        public static string str;

        public static async void addnewnoteButton_Clicked(object sender, EventArgs e)
        {
            nameEntry.Text = nameEntry.Text.TrimEnd();
            if (String.IsNullOrEmpty(commentEntry.Text) == false)
                commentEntry.Text = commentEntry.Text.Trim();
            str = null;
            for (int i = 0; i <= count_subtasks - 1; i++)
            {
                if (String.IsNullOrWhiteSpace(((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text) == false)
                {
                    if (((subtasksSL.Children[i] as StackLayout).Children[0] as CustomButton).Text == "\uE73E")
                        //((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text = "➥" + Addition.ConvertToStrikethrough(((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text);
                        ((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text = "➥" + ((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text;

                    ((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text = ((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text.Trim();
                    str += ((subtasksSL.Children[i] as StackLayout).Children[1] as CustomEditor2).Text + "✖";
                }
            }

            if (edit == false)
            {
                editnote2 = new NoteVM
                {
                    Name = nameEntry.Text,
                    Comment = commentEntry.Text,
                    Date = datePicker.Date,
                    ColorMarker_string = Addition.HexConverter(additionButton.BackgroundColor),
                    Repeat = repeatSwitch.IsToggled,
                    Subtasks_string = str
                };

                await App.Database.SaveItemAsync(editnote2);

                NewNotePage_UWP.count_subtasks = 0;
                NewNotePage_UWP.str = null;
                NewNotePage_UWP.str_old = null;
                NewNotePage_UWP.subtasksSL.Children.Clear();
                NewNotePage_UWP.subtasksSL.Children.Add(NewNotePage_UWP.subdefaultSL);
            }
            else
            {
                editnote.Name = nameEntry.Text;
                editnote.Comment = commentEntry.Text;
                editnote.Date = datePicker.Date;
                editnote.ColorMarker_string = Addition.HexConverter(additionButton.BackgroundColor);
                editnote.Repeat = repeatSwitch.IsToggled;
                editnote.Subtasks_string = str;

                if (editnote.Date >= DateTime.Today)
                    editnote.IsOverdue = false;
                else
                    editnote.Overdue_str = "(от " + editnote.Date.ToString("dd MMMM") + ")";

                await App.Database.SaveItemAsync(editnote);

                newnoteSL.IsVisible = false;
                MainPage_UWP.addImage.Source = "images/plus.png";
            }

            Common();
        }

        public static async void removeButton_Clicked(object sender, EventArgs e)
        {
            await App.Database.DeleteItemAsync(editnote);

            newnoteSL.IsVisible = false;
            MainPage_UWP.addImage.Source = "images/plus.png";
            Common();
        }

        public static void BackButton_Clicked(object sender, EventArgs e)
        {
            newnoteSL.IsVisible = false;
            MainPage_UWP.addImage.Source = "images/plus.png";
            Common();
        }
        
        public static void Common()
        {
            if (edit == true)
                edit = false;

            nameEntry.Text = "";
            commentEntry.Text = "";
            datePicker.Date = DateTime.Today;
            datePicker.MinimumDate = DateTime.Today;
            str = null;
            str_old = null;

            foreach (Button btn in chooseColorSL.Children)
            {
                btn.Text = "";
                lastButton_int = 0;
            }
            colorButton0.Text = "\uE73E";
            lastButton_int = colorButton0.GetHashCode();
            additionButton.BackgroundColor = colorButton0.BackgroundColor;

            addnewnoteButton.Text = "Добавить";
            removeButton.IsVisible = false;
            repeatSwitch.IsToggled = false;
        }

        public static void ColorButton_Clicked(object sender, EventArgs e)
        {
            foreach (Button btn in chooseColorSL.Children)
            {
                if (btn.GetHashCode() == lastButton_int)
                    btn.Text = "";
            }
            var button = sender as Button;
            button.Text = "\uE73E";
            lastButton_int = button.GetHashCode();
            additionButton.BackgroundColor = button.BackgroundColor;
        }

        public static void StackLayout_SizeChanged(object sender, EventArgs e)
        {
            //if (StackLayout.Height >= MainPage_UWP.ScreenDimensions.Height - 47.5)
            //    StackLayout.HeightRequest = MainPage_UWP.ScreenDimensions.Height - 47.5;
            //else
            //    StackLayout.HeightRequest = -1;
        }

        public static void NewnoteSL_SizeChanged(object sender, EventArgs e)
        {
            if (newnoteSL.Height > MainPage_UWP.ScreenDimensions.Height - 47.5)
                newnoteSL.HeightRequest = MainPage_UWP.ScreenDimensions.Height - 47.5;
            else
                newnoteSL.HeightRequest = -1;
        }

        public static void NewnoteSL_RefreshSize()
        {
            if (newnoteSL.Height > MainPage_UWP.ScreenDimensions.Height - 47.5)
                newnoteSL.HeightRequest = MainPage_UWP.ScreenDimensions.Height - 47.5;
            else
                newnoteSL.HeightRequest = -1;
        }
    }
}