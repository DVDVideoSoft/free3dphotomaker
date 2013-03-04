using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DVDVideoSoft.AppFxApi
{
    public enum ProgressDisplayMode
    {
        None,
        ProgressSimple,
        ProgressDetailed,
        Completed
    }

    public enum ProgressColorKind
    {
        Success,
        Error,
        Current, 
        Cancel
    }

    public interface IProgressDlg
    {
        void SetCaptionBase(string value, bool applyImmediately);

        void SetProgress(int value);
        void SetProgress(double value);
        void SetTotalProgress(int value);

        void HideAndClear();
        void AddProcessString(string strText);
        void AddProcessString(ProcessingString strText, bool bWithoutPath = false);
        void AddProcessString(string strText, Color color);
        void RemoveLastProcessString();

        bool ShowProgressInCaption { get; set; }
        bool Ready { get; }
        ProgressDisplayMode DisplayMode { get; set; }
        bool MarqueeMode { get; set; }

        void EnableBtnClose(bool enable);

        Color GetColor(ProgressColorKind colorKind);
    }
}
