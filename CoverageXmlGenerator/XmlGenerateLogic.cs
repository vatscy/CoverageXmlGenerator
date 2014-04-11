using System.Collections;
using System.IO;
using CoverageXmlGenerator.Properties;
using Microsoft.VisualStudio.Coverage.Analysis;

namespace CoverageXmlGenerator
{
	/// <summary>
	/// coveragexmlファイルの生成に関連するメソッドを提供します。
	/// </summary>
	public static class XmlGenerateLogic
	{
		private static readonly string DefalutSearchRootPath = ".";

		/// <summary>
		/// .coverageファイルを検索します。
		/// </summary>
		/// <param name="searchRootPath">検索する際のルートディレクトリパス</param>
		/// <returns>.coverageファイルパス</returns>
		public static string FindCoverageFile(string searchRootPath)
		{
			searchRootPath = searchRootPath ?? DefalutSearchRootPath;
			var dirQueue = new Queue();
			dirQueue.Enqueue(searchRootPath);

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

		/// <summary>
		/// .coveragexmlファイルを出力します。
		/// </summary>
		/// <param name="coverageFilePath">.coverageファイルパス</param>
		/// <param name="xmlFileName">生成するファイル名</param>
		/// <returns></returns>
		public static string GenerateXml(string coverageFilePath, string xmlFileName)
		{
			var generatedFileName = xmlFileName ?? Settings.Default.DefaultGeneratedFineName;
			using (var info = CoverageInfo.CreateFromFile(coverageFilePath, new string[] { }, new string[] { }))
			{
				var coverageDataSet = info.BuildDataSet();
				coverageDataSet.WriteXml(generatedFileName);
			}
			return generatedFileName;
		}
	}
}
