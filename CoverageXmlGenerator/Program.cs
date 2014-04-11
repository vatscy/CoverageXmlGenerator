using System;
using CoverageXmlGenerator.Properties;

namespace CoverageXmlGenerator
{
	/// <summary>
	/// 実行クラスです。
	/// </summary>
	public class Program
	{
		/// <summary>
		/// 実行メソッドです。
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			try
			{
				var searchRootPath = args.Length > 0 ? args[0] : null;
				var coverageFilePath = XmlGenerateLogic.FindCoverageFile(searchRootPath);
				Console.WriteLine(Settings.Default.Found + coverageFilePath);

				var xmlFileName = args.Length > 1 ? args[1] : null;
				var generatedFileName = XmlGenerateLogic.GenerateXml(coverageFilePath, xmlFileName);
				Console.WriteLine(Settings.Default.Created + generatedFileName);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
