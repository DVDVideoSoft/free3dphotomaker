using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.AppFxApi
{
    public class ProgressEventArgs : EventArgs
    {
        private int    itemIndex;
        private int    progressPercentage;
        private double progressDouble;

        public ProgressEventArgs(int itemIndex, int progressPercentage, double progressDouble)
        {
            this.itemIndex = itemIndex;
            this.progressPercentage = progressPercentage;
            this.progressDouble = progressDouble;
        }

        public int ItemIndex { get { return this.itemIndex; } }
        public int ProgressPercentage { get { return this.progressPercentage; } }
        public double ProgressDouble { get { return this.progressDouble; } }
    }

    public class ItemProcessedEventArgs : EventArgs
    {
        private int index;
        private object state;

        public int Index { get { return this.index; } }
        public object State { get { return this.state; } }

        public ItemProcessedEventArgs(int index, object state)
        {
            this.index = index;
            this.state = state;
        }
    }
}
