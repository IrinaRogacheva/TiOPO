using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TVSet;

namespace TVSetTests
{
    [TestClass]
    public class ThenTVIsTurnedOff
    {
        TVSet.TVSet tv;

        [TestInitialize]
        public void TestInit()
        {
            tv = new TVSet.TVSet();
        }

        [TestMethod]
        public void GetDefaultStateOfTV_TVIsTurnedOff()
        {
            Assert.IsTrue(tv.IsTurnedOn() == false);
        }

        [TestMethod]
        public void GetDefaultChannelThenTVIsTurnedOff_DefaultChannelIsZero()
        {
            Assert.IsTrue(tv.GetChannel() == 0);
        }

        [TestMethod]
        public void TryToSelectChannel_ChannelIsZero()
        {
            tv.SelectChannel(87);
            Assert.IsTrue(tv.GetChannel() == 0);
        }

        [TestMethod]
        public void TryToSelectPreviousChannel_NullIsReturned()
        {
            Assert.IsTrue(tv.SelectPreviousChannel() == false);
        }

        [TestMethod]
        public void TryToSelectChannelAfter_NullIsReturned()
        {
            Assert.IsTrue(tv.SelectChannelAfter() == false);
        }

        [TestMethod]
        public void TryToSelectChannelBefore_NullIsReturned()
        {
            Assert.IsTrue(tv.SelectChannelBefore() == false);
        }

        [TestMethod]
        public void TurnOnTV_TVIsTurnedOn()
        {
            tv.TurnOn();
            Assert.IsTrue(tv.IsTurnedOn());
        }
    }

    [TestClass]
    public class ThenTVIsTurnedOn
    {
        TVSet.TVSet tv;

        [TestInitialize]
        public void TestInit()
        {
            tv = new TVSet.TVSet();
            tv.TurnOn();
        }

        [TestMethod]
        public void GetDefaultStateOfTVThenTVIsTurnedOn_ChannelIsFirst()
        {
            Assert.IsTrue(tv.GetChannel() == 1);
        }

        [TestMethod]
        public void SelectMaxChannel_ChannelISSelected()
        {
            tv.SelectChannel(99);
            Assert.IsTrue(tv.GetChannel() == 99);
        }

        [TestMethod]
        public void SelectChannelInMiddle_ChannelISSelected()
        {
            tv.SelectChannel(42);
            Assert.IsTrue(tv.GetChannel() == 42);
        }

        [TestMethod]
        public void SelectChannelLessThanMin_ChannelIsNotSelected()
        {
            tv.SelectChannel(0);
            Assert.IsTrue(tv.GetChannel() == 1);
        }

        [TestMethod]
        public void SelectChannelBiggerThanMax_ChannelIsNotSelected()
        {
            tv.SelectChannel(100);
            Assert.IsTrue(tv.GetChannel() == 1);
        }

        [TestMethod]
        public void SelectPreviousChannel_PreviousChannelIsSelected()
        {
            tv.SelectChannel(8);

            tv.SelectChannel(25);
            tv.SelectPreviousChannel();

            Assert.IsTrue(tv.GetChannel() == 8);
        }

        [TestMethod]
        public void SelectChannelAfter_ChannelAfterIsSelected()
        {
            tv.SelectChannel(25);

            tv.SelectChannelAfter();
            Assert.IsTrue(tv.GetChannel() == 26);
        }

        [TestMethod]
        public void SelectChannelAfterMax_ChannelAfterIsSelected()
        {
            tv.SelectChannel(99);

            tv.SelectChannelAfter();
            Assert.IsTrue(tv.GetChannel() == 1);
        }

        [TestMethod]
        public void SelectChannelBefore_ChannelBeforeIsSelected()
        {
            tv.SelectChannel(25);

            tv.SelectChannelBefore();
            Assert.IsTrue(tv.GetChannel() == 24);
        }

        [TestMethod]
        public void SelectChannelBeforeMin_ChannelAfterIsSelected()
        {
            tv.SelectChannelBefore();
            Assert.IsTrue(tv.GetChannel() == 99);
        }

        [TestMethod]
        public void TurnOffTV_TVIsTurnedOff()
        {
            tv.TurnOff();
            Assert.IsTrue(!tv.IsTurnedOn());
        }
    }
}
