using System;
using Eval.Interfaces;
using Xamarin.Forms;
using Eval.Droid;
using System.IO;

[assembly: Dependency(typeof(FileHelper))]
namespace Eval.Droid
{
	public class FileHelper : IFileHelper
	{
		public string GetLocalFilePath(string filename)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			return Path.Combine(path, filename);
		}
	}
}
