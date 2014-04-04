using System;
using System.Collections;
using System.IO;
using Microsoft.VisualStudio.Coverage.Analysis;

namespace CoverageXmlGenerator
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var dir = ".";
			if (args.Length > 0)
			{
				dir = args[0];
			}

			try
			{
				var coverageFilePath = FindCoverageFile(dir);
				Console.WriteLine("Found Coverage File: " + coverageFilePath);
				using (var info = CoverageInfo.CreateFromFile(coverageFilePath, new string[] { }, new string[] { }))
				{
					var coverageDataSet = info.BuildDataSet();
					var fileName = "result";
					if (args.Length > 1)
					{
						fileName = args[1];
					}
					coverageDataSet.WriteXml(fileName + ".coveragexml");
					Console.WriteLine("Created Coverage Xml File: " + fileName + ".coveragexml");
				}
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (CoverageFileNotFoundException e)
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
					if (file.ToLower().EndsWith(".coverage"))
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
			throw new FileNotFoundException("Not Found Coverage File.");
		}
	}
}
