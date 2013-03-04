using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using DVDVideoSoft.Utils;
using DVDVideoSoft.AppFxApi;

namespace Free3DPhotoMaker
{
    public class Configuration
    {
        public static string OutputFile = "OutputFile";
        public static string AvailableAlgorithms="Algorithms";
        public static string Logger = "Log";
        public static string UseMultiCore = "MultiCore";
        public static string MainFormName = "MainFormName";
        public static string ShowPreview = "ShowPreview";
        public static string DoNotShowWelcomeScreen = "DoNotShowWelcome";
        public static string FirstRun = "FirstRun";
        public static string UseSingleImage = "UseSingleImage";
        
        private PropsProvider configHolder;

        private RegistryPropsReader regReader;
        private RegistryPropsWriter regWriter;

        private string outputFolder;

        public Configuration(string regPath, string defaultOutputFolder)
        {
            regReader = new RegistryPropsReader(regPath);
            regWriter = new RegistryPropsWriter(regPath);

            configHolder = new PropsProvider(regReader);
            
            this.outputFolder = this.regReader.Get<string>(Defs.PN.OutputFolder.ToString(), defaultOutputFolder);
        }

        public void InitStorableKeyList()
        {
            string outputFile = Path.Combine(this.outputFolder, "3DImage.jpg");
            ((IPropsSaveToHelper)configHolder).AddStorableValue(OutputFile, outputFile);
            ((IPropsSaveToHelper)configHolder).AddStorableValue(Defs.PN.OutputFolder.ToString(), this.outputFolder);
            ((IPropsSaveToHelper)configHolder).AddStorableValue(UseMultiCore, true);
            ((IPropsSaveToHelper)configHolder).AddStorableValue(ShowPreview, true);
            ((IPropsSaveToHelper)configHolder).AddStorableValue(DoNotShowWelcomeScreen, false);
            ((IPropsSaveToHelper)configHolder).AddStorableValue(FirstRun, true);
            ((IPropsSaveToHelper)configHolder).AddStorableValue(UseSingleImage, false);
        }

        public PropsProvider ConfigHolder
        {
            get { return configHolder; }
            set { configHolder = value; }
        }

        public void Save()
        {
            ((IPropsSaveToHelper)configHolder).SaveTo(regWriter);
        }
    }
}
