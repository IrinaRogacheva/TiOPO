using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVSet
{
    public class TVSet
    {
        private bool m_isOn = false;
        private int m_selectedChannel = 1;

        public bool IsTurnedOn()
        {
            return m_isOn;
        }

        public void TurnOn()
        {
            m_isOn = true;
        }

        public void TurnOff()
        {
            m_isOn = false;
        }

        public int GetChannel()
        {
            return m_isOn? m_selectedChannel : 0;
        }

        public bool SelectChannel(int channel)
        {
            bool isAvailableChannel = (channel >= 1) && (channel <= 99);
            if (isAvailableChannel && m_isOn)
            {
                m_selectedChannel = channel;
                return true;
            }
            return false;
        }
    }
}
