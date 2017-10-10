using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Compression;
using DublettenChecker;
using System.Collections.Generic;
using DublettenCheckerTest.Helper;

namespace DublettenCheckerTest
{
    [TestClass]
    public class FileDublettenCheckerTest
    {

        [ClassInitialize()]
        [DeploymentItem(@"TestResources\Suchordner.zip", "resource")]
        public static void FileDublettenCheckerTestClassInitialize(TestContext testContext)
        {

            if (!Directory.Exists(@".\Suchordner") && File.Exists(@"resource\Suchordner.zip"))
            {
                ZipFile.ExtractToDirectory(@"resource\Suchordner.zip", ".");
            }
        }

        private FileDublettenChecker _fileDublettenChecker;
        [TestInitialize()]
        public void FileDublettenCheckerTestInitialize()
        {

            _fileDublettenChecker = new FileDublettenChecker();
        }

        [TestMethod]
        [DeploymentItem(@"TestResources\Suchordner.zip", "resource")]
        public void FileDublettenCheckerFindsTwoCandidatesWithDifferentNamesTest()
        {
            string searchPath = @".\Suchordner\SubA";
            string expected1 = @"\DoppeltesFoto.JPG";
            string expected2 = @"\_DSC0314.JPG";

            IEnumerable<IDublette> actual = _fileDublettenChecker.Sammle_Kandidaten(searchPath, VergleichsModi.Size);

            List<IDublette> l = new List<IDublette>(actual);
            Assert.AreEqual(l.Count, 1, "Expected one collection of candidates");
            var dateiPfade = new List<string>(l[0].Dateipfade);
            Assert.AreEqual(dateiPfade.Count, 2, "Expected 2 matching candidates.");
            Assert.IsTrue(dateiPfade.Contains(searchPath + expected1));
            Assert.IsTrue(dateiPfade.Contains(searchPath + expected2));
        }

        [TestMethod]
        [DeploymentItem(@"TestResources\Suchordner.zip")]
        public void FileDublettenCheckerFindsTwoRealDublicatesTest()
        {
            string searchPath = @".\Suchordner\SubA";
            string expected1 = @"\DoppeltesFoto.JPG";
            string expected2 = @"\_DSC0314.JPG";

            List<IDublette> actual = new List<IDublette>(_fileDublettenChecker.Sammle_Kandidaten(searchPath, VergleichsModi.Size));
            Assert.AreEqual(actual.Count, 1, "Expected one collection of candidates");
            var dateiPfade = new List<string>(actual[0].Dateipfade);
            Assert.AreEqual(dateiPfade.Count, 2, "Expected 2 matching candidates.");
            Assert.IsTrue(dateiPfade.Contains(searchPath + expected1));
            Assert.IsTrue(dateiPfade.Contains(searchPath + expected2));
        }

        [TestMethod]
        [DeploymentItem(@"TestResources\Suchordner.zip", "resource")]
        public void FileDublettenCheckerSizeAndNameReturnsNoMatchingTest()
        {
            string searchPath = @".\Suchordner\SubA";

            List<IDublette> actual = new List<IDublette>(_fileDublettenChecker.Sammle_Kandidaten(searchPath, VergleichsModi.SizeAndName));
            Assert.AreEqual(actual.Count, 0, "Expected no collection of candidates");
        }



        [TestMethod]
        public void FileDublettenCheckerPruefeKandidatenAreMatchingTest()
        {
            string expectePath1 = "path_of_File1";
            string expectedPath2 = "path_of_File2";

            var kandidaten = DublettenTestHelper.GetMatchingDubletteWithDifferentFilenames();
            var hits = (List<IDublette>)_fileDublettenChecker.Pruefe_Kandidaten(kandidaten);
            Assert.IsTrue(hits.Count == 1, "Expected one match");
            Assert.IsTrue((hits[0].Dateipfade as List<string>).Contains(expectePath1));
            Assert.IsTrue((hits[0].Dateipfade as List<string>).Contains(expectedPath2));
        }

        [TestMethod]
        [DeploymentItem(@"TestResources\Suchordner.zip", "resource")]
        public void FileDublettenCheckerSizeCompleteTest()
        {
            string searchPath = @".\Suchordner\";
            List<IDublette> kandidaten = new List<IDublette>(_fileDublettenChecker.Sammle_Kandidaten(searchPath, VergleichsModi.Size));
            List<IDublette> hits = (List<IDublette>)_fileDublettenChecker.Pruefe_Kandidaten(kandidaten);
            Assert.AreEqual(hits.Count, 5);
        }

        [TestMethod]
        [DeploymentItem(@"TestResources\Suchordner.zip", "resource")]
        public void FileDublettenCheckerSizeAndNameCompleteTest()
        {
            string searchPath = @".\Suchordner\";
            List<IDublette> kandidaten = new List<IDublette>(_fileDublettenChecker.Sammle_Kandidaten(searchPath, VergleichsModi.SizeAndName));
            List<IDublette> hits = (List<IDublette>)_fileDublettenChecker.Pruefe_Kandidaten(kandidaten);
            Assert.AreEqual(hits.Count, 4);
        }
    }
}
