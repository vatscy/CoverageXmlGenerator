using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoverageXmlGenerator.Tests
{
	[TestClass]
	public class XmlGenerateLogicTest
	{
		[TestMethod]
		public void FindCoverageFile_正常系_null指定_ファイル存在_ファイルパスが返る()
		{
		}

		[TestMethod]
		public void FindCoverageFile_正常系_ファイルパスを指定_ファイル存在_ファイルパスが返る()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void FindCoverageFile_正常系_ファイル無し_例外が発生する()
		{
		}

		[TestMethod]
		public void FindCoverageFile_正常系_null指定_デフォルトファイル名が返る()
		{
		}

		[TestMethod]
		public void FindCoverageFile_正常系_ファイル名を指定_指定したファイル名が返る()
		{
		}
	}
}
