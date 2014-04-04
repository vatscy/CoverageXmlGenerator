using System;
using System.Collections;
using System.IO;
using CoverageXmlGenerator.Properties;
using Microsoft.VisualStudio.Coverage.Analysis;

namespace CoverageXmlGenerator
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var searchRootPath = ".";
			if (args.Length > 0)
			{
				searchRootPath = args[0];
			}

			try
			{
				var coverageFilePath = FindCoverageFile(searchRootPath);
				Console.WriteLine(Settings.Default.Found + coverageFilePath);
				using (var info = CoverageInfo.CreateFromFile(coverageFilePath, new string[] { }, new string[] { }))
				{
					var coverageDataSet = info.BuildDataSet();
					var generatedFileName = Settings.Default.DefaultGeneratedFineName;
					if (args.Length > 1)
					{
						generatedFileName = args[1];
					}
					coverageDataSet.WriteXml(generatedFileName);
					Console.WriteLine(Settings.Default.Created + generatedFileName);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private static string FindCoverageFile(string dir)
		{
			var dirQueue = new Queue();
			dirQueue.Enqueue(dir);

			while (dirQueue.Count > 0)
			{
				var d = dirQueue.Dequeue() as string;

				var files = Directory.GetFiles(d);
				foreach (var file in files)
				{
					if (file.ToLower().EndsWith(Settings.Default.CoverageFileExtension))
					{
						return file;
					}
				}

				var dirs = Directory.GetDirectories(d);
				foreach (var next in dirs)
				{
					dirQueue.Enqueue(next);
				}
			}
			throw new FileNotFoundException(Settings.Default.NotFound);
		}
	}
}
