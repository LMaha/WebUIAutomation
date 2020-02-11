using NUnit.Framework;
using System.Threading;

namespace GoShipUI
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class GoShipUI_FTLNoLoadsTests : BaseTest
    {
        [Test]
        //GS-648-TC_01_FlatBed_Length
        //invalid credentials for Length(in Inches) are passed as an argument
        public void DimsErrorMessageLenghtFlatbedTest()
        {
            HomePage.LoginFTL("Flatbed")
                .SignIn().EnterDetailsFTL()
                .CheckErrorMessageLenghtFTL("637");
        }

        [Test]
        //GS-648-TC_02_FlatBed_Width
        //invalid credentials for Width(in Inches) are passed as an argument
        public void DimsErrorMessageWidthFlatbedTest()
        {
            HomePage.LoginFTL("Flatbed")
                .SignIn().EnterDetailsFTL()
                .CheckErrorMessageWidthFTL("103");
        }


        [Test]
        //GS-648-TC_03_FlatBed_Height
        //invalid credentials for Height(in Inches) are passed as an argument
        public void DimsErrorMessageHeightFlatbedTest()
        {
            HomePage.LoginFTL("Flatbed")
              .SignIn().EnterDetailsFTL()
              .CheckErrorMessageHeightFTL("103");
        }

        [Test]
        //GS-648-TC_04_FlatBed_Weight
        //invalid credentials for Weight(in Pounds/lb) are passed as an argument
        public void DimsErrorMessageWeightFlatbedTest()
        {
            HomePage.LoginFTL("Flatbed")
              .SignIn().EnterDetailsFTL()
              .CheckErrorMessageWeighttFTL_FlatBed("47501");
        }

        [Test]
        //GS-648-TC_05_Van_Length
        //invalid credentials for Length(in Inches) are passed as an argument
        public void DimsErrorMessageLenghtVanTest()
        {
            // HomePage.CheckErrorMessageLenghtFTL("Van", "613");
            HomePage.LoginFTL("Van")
                .SignIn().EnterDetailsFTL()
                .CheckErrorMessageLenghtFTL("613");

        }

        [Test]
        //GS-648-TC_06_Van_Width
        //invalid credentials for Width(in Inches) are passed as an argument
        public void DimsErrorMessageWidthVanTest()
        {
            HomePage.LoginFTL("Van")
               .SignIn().EnterDetailsFTL()
               .CheckErrorMessageWidthFTL("102");
        }

        [Test]
        //GS-648-TC_07_Van_Height
        //invalid credentials for Height(in Inches) are passed as an argument
        public void DimsErrorMessageHeightVanTest()
        {
            HomePage.LoginFTL("Van")
                .SignIn().EnterDetailsFTL()
                .CheckErrorMessageHeightFTL("111");
        }

        [Test]
        //GS-648-TC_08_Van_Weight
        //invalid credentials for Weight(in Pounds/lb) are passed as an argument
        public void DimsErrorMessageWeightVanTest()
        {
            HomePage.LoginFTL("Van")
                .SignIn().EnterDetailsFTL()
                .CheckErrorMessageWeighttFTL_Van("45001");
        }

        [Test]
        //GS-648-TC_09_Reefer_Length
        //invalid credentials for Length(in Inches) are passed as an argument
        public void DimsErrorMessageLenghtReeferTest()
        {
            HomePage.LoginFTL("Reefer")
                .SignIn().EnterDetailsFTL()
                .CheckErrorMessageLenghtFTL("613");
        }

        [Test]
        //GS-648-TC_10_Reefer_Width
        //invalid credentials for Width(in Inches) are passed as an argument
        public void DimsErrorMessageWidthReeferTest()
        {
            HomePage.LoginFTL("Reefer")
                .SignIn().EnterDetailsFTL()
                .CheckErrorMessageWidthFTL("98");
        }

        [Test]
        //GS-648-TC_11_Reefer_Height
        //invalid credentials for Height(in Inches) are passed as an argument
        public void DimsErrorMessageHeightReeferTest()
        {
            HomePage.LoginFTL("Reefer")
                .SignIn().EnterDetailsFTL()
                .CheckErrorMessageHeightFTL("99");
        }

        [Test]
        //GS-648-TC_12_Reefer_Weight
        //invalid credentials for Weight(in Pounds/lb) are passed as an argument
        public void DimsErrorMessageWeightReeferTest()
        {
            HomePage.LoginFTL("Reefer")
                 .SignIn().EnterDetailsFTL()
                 .CheckErrorMessageWeighttFTL_Reefer("43501");

        }
    }
}
