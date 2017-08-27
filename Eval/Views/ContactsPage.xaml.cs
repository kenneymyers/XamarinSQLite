using System;
using System.Collections.Generic;
using Eval.Controllers;
using Eval.Models;
using Xamarin.Forms;

namespace Eval.Views
{
    public partial class ContactsPage : ContentPage
    {
        public List<Contact> contactList { get; set; }

        public ContactsPage()
        {
            this.BindingContext = this;

            InitializeComponent();

            SQLiteController sc = new SQLiteController();
            sc.CreateAll();
            sc.DeleteAll();
            sc.InsertAll();
            contactList = sc.GetAllContacts();

            //Set the items source for the listview
            lstContacts.ItemsSource = contactList;
 
        }

        public async void StartComplete(){
			Device.BeginInvokeOnMainThread(async () => {
				await this.DisplayAlert("Made it here", "Okay",
											"OK");
			});
        }
    }
}
