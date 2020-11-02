using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Xml;

namespace Assignment2
{
    //Assignment_2 done by:

    //Andrii Kosenko
    //301097272

    //Mucahit Aric 
    //301090476

    //Tereza Konstari 
    //301065539

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///    

    public partial class MainWindow : Window
    {
        public double Sub = 0;

        public static List<Deserts> desserts = new List<Deserts> {
            new Deserts() {OrderName="Apple Pie",Price=5.95,Catagory="Desert"},
            new Deserts() {OrderName="Sundae",Price=3.95,Catagory="Desert"},
            new Deserts() {OrderName="Carrot Cake",Price=5.95,Catagory="Desert"},
            new Deserts() {OrderName="Mud Pie",Price=4.95,Catagory="Desert"},
            new Deserts() {OrderName="Apple Crisp",Price=5.95,Catagory="Desert"}
        };

        public static List<Drinks> drinksList = new List<Drinks>
        {
            new Drinks(){ OrderName="Soda", Price=1.95, Catagory="Drink" },
            new Drinks() {OrderName="Tea", Price=1.5,Catagory="Drink" },
            new Drinks() {OrderName="Coffee", Price=1.25,Catagory="Drink" },
            new Drinks() {OrderName="Mineral Water", Price=2.95,Catagory="Drink" },
            new Drinks() {OrderName="Juice", Price=2.25,Catagory="Drink" },
            new Drinks() {OrderName="Milk", Price=1.5,Catagory="Drink" }
        };

        public static List<Appetizers> appetizers = new List<Appetizers>
       {
           new Appetizers() {OrderName="Buffalo Wings", Price=5.95,Catagory="Appetizer" },
            new Appetizers() {OrderName="Potato Skins", Price=6.95,Catagory="Appetizer" },
            new Appetizers() {OrderName="Nachos", Price=8.95,Catagory="Appetizer" },
            new Appetizers() {OrderName="Mushroom Caps", Price=10.95,Catagory="Appetizer" },
            new Appetizers() {OrderName="Shrimp Cocktail", Price=12.95,Catagory="Appetizer" },
            new Appetizers() {OrderName="Chips and Salsa", Price=6.95,Catagory="Appetizer" }
        };

        public static List<MainCourses> mainCourses = new List<MainCourses>
        {
            new MainCourses() {OrderName="Seafood Alfredo", Price=15.95,Catagory="MainCourse" },
            new MainCourses() {OrderName="Chicken Alfredo", Price=13.95,Catagory="MainCourse" },
            new MainCourses() {OrderName="Chicken Picatta", Price=13.95,Catagory="MainCourse" },
            new MainCourses() {OrderName="Turkey Club", Price=11.95,Catagory="MainCourse" },
            new MainCourses() {OrderName="Lobster Pie", Price=19.95,Catagory="MainCourse" },
            new MainCourses() {OrderName="Prime Rib", Price=20.95,Catagory="MainCourse" },
            new MainCourses() {OrderName="Shrimp Scampi", Price=18.95,Catagory="MainCourse" },
            new MainCourses() {OrderName="Turkey Dinner", Price=13.95,Catagory="MainCourse" },
            new MainCourses() {OrderName="Stuffed Chicken", Price=14.95,Catagory="MainCourse" },
        };

        public MainWindow()
        {
            InitializeComponent();
            lblTitle.Content = "Product \t\t Price \t\t Category \n";
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e){ }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e){ }

        private void comboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e) { }


        int counter = 20;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Bill";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Arial", 14, XFontStyle.Regular);

            graph.DrawString("Your Bill", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

            foreach (var item in orderListV.Items)
            {
                graph.DrawString($"{item}", font, XBrushes.Black, new XRect(0, counter += 20, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            }

            graph.DrawString($"Subtotal: {label10.Content}", font, XBrushes.Black, new XRect(0, counter += 50, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString($"HST: {label12.Content}", font, XBrushes.Black, new XRect(0, counter += 20, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString($"Total: {label11.Content}", font, XBrushes.Black, new XRect(0, counter += 20, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

            string pdfFilename = "bill.pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            comboBox.SelectedItem = null;
            comboBox1.SelectedItem = null;
            comboBox3.SelectedItem = null;
            comboBox2.SelectedItem = null;
            textBox.Text = " ";
            label10.Content = " ";
            label11.Content = " ";
            label12.Content = " ";
            Sub = 0;
            orderListV.Items.Clear();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e) { }

        private void CalculateSubtotal()
        {
            label10.Content = Sub.ToString();
            label12.Content = (Sub * 0.13).ToString();
            label11.Content = (Sub + (Sub * 0.13)).ToString();
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            foreach (Drinks drinks in drinksList)
            {
                if (comboBox.Text == drinks.OrderName)
                {
                    orderListV.Items.Add($"{drinks.OrderName}\t\t{drinks.Price}\t\t{drinks.Catagory}");
                    CalculateSubtotal();
                    drinks.Subtotal += drinks.Price;
                    Sub += drinks.Subtotal;                   
                }
            }
            CalculateSubtotal();
        }

        private void ComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) { }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(Drinks drink in drinksList)
            {
                comboBox.Items.Add(drink.OrderName.ToString());
            }
        }

        private void ComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            foreach (Appetizers appetizers in appetizers)
            {
                if (comboBox1.Text == appetizers.OrderName)
                {
                    orderListV.Items.Add($"{appetizers.OrderName}\t\t{appetizers.Price}\t\t{appetizers.Catagory}");
                    appetizers.Subtotal += appetizers.Price;
                    Sub += appetizers.Subtotal;
                }
            }
            CalculateSubtotal();
        }

        private void ComboBox1_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Appetizers appetizers in appetizers)
            {
                comboBox1.Items.Add(appetizers.OrderName.ToString());
            }
        }

        private void ComboBox1_DropDownOpened(object sender, EventArgs e)
        {
            foreach (Appetizers appetizers in appetizers)
            {
                if (comboBox1.Text == appetizers.OrderName)
                {
                    orderListV.Items.Remove($"{appetizers.OrderName}\t\t{appetizers.Price}\t\t{appetizers.Catagory}");
                }
            }
        }

        private void ComboBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
           
                foreach (Drinks drinks in drinksList)
                {
                    if (comboBox.Text == drinks.OrderName)
                    {
                        orderListV.Items.Remove($"{drinks.OrderName}\t\t{drinks.Price}\t\t{drinks.Catagory}");
                    }
                }            
        }

        private void ComboBox3_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(MainCourses mainCourses in mainCourses)
            {
                comboBox3.Items.Add(mainCourses.OrderName.ToString());
            }
        }

        private void ComboBox2_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Deserts dessert in desserts)
            {
                comboBox2.Items.Add(dessert.OrderName.ToString());
            }
        }

        private void ComboBox3_DropDownClosed(object sender, EventArgs e)
        {
            foreach (MainCourses mainCourses in mainCourses)
            {
                if (comboBox3.Text == mainCourses.OrderName)
                {
                    orderListV.Items.Add($"{mainCourses.OrderName}\t\t{mainCourses.Price}\t\t{mainCourses.Catagory}");
                    mainCourses.Subtotal += mainCourses.Price;
                    Sub += mainCourses.Subtotal;
                }
            }
            CalculateSubtotal();
        }

        private void ComboBox2_DropDownClosed(object sender, EventArgs e)
        {
            foreach (Deserts deserts in desserts)
            {
                if (comboBox2.Text == deserts.OrderName)
                {
                    orderListV.Items.Add($"{deserts.OrderName}\t\t{deserts.Price}\t\t{deserts.Catagory}");
                    deserts.Subtotal += deserts.Price;
                    Sub += deserts.Subtotal;
                }
            }
            CalculateSubtotal();
        }
    }
}
