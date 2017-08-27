using System;
using System.Collections.Generic;
using Eval.Interfaces;
using Eval.Models;
using SQLite;
using Xamarin.Forms;

namespace Eval.Controllers
{
	public class SQLiteController
	{

		static object locker = new object();
		SQLiteConnection database;

		public SQLiteController()
		{
			database = new SQLiteConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("sqlite.sqlite"));
		}

		public void CreateAll()
		{
            lock (locker)
            {
                //Create all tables we need if they don't exist
                database.CreateTable<Contact>();
            }
		}

		public void DeleteAll()
		{
            lock (locker)
            {
                //If we ever want to wipe everything
                database.DeleteAll<Contact>();
            }
		}

		public void InsertAll()
		{
            lock (locker)
            {
                //Create a couple contacts
                Contact contact = new Contact();
                contact.FirstName = "John";
                contact.LastName = "Smith";
                contact.Email = "john@smith.com";
                SaveContact(contact);

				//Create a couple contacts
				contact = new Contact();
				contact.FirstName = "Jane";
				contact.LastName = "Doe";
				contact.Email = "jane@doe.com";
				SaveContact(contact);

            }
		}

		public bool TableExists(string tab)
		{
			lock (locker)
			{
				string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name='" + tab + "'";
				var cmd = database.CreateCommand(cmdText, tab);
				return cmd.ExecuteScalar<string>() != null;
			}
		}

		public List<Contact> GetAllContacts()
		{
			lock (locker)
			{
				return database.Query<Contact>("SELECT * FROM Contact");
			}
		}

		//RUD on ScoreType
		public Contact GetContact(int id)
		{
			return database.Table<Contact>().Where(i => i.Id == id).FirstOrDefault();
		}

		public bool ContactExists(Contact contact)
		{
			string cmdText = "SELECT LastName FROM Contact WHERE Id=" + contact.Id.ToString();
			var cmd = database.CreateCommand(cmdText, contact.LastName);
			return cmd.ExecuteScalar<string>() != null;
		}

		public void SaveContact(Contact contact)
		{
			lock (locker)
			{
				if (ContactExists(contact))
				{
					database.Update(contact);
				}
				else
				{
					database.Insert(contact);
				}
			}
		}

		public void DeleteContact(Contact contact)
		{
			lock (locker)
			{
				database.Delete(contact);
			}
		}


	}
}
