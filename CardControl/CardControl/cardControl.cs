using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardControl
{
    public partial class Card: UserControl
    {
        public Card()
        {
            InitializeComponent();
        }

        public Card(int id, bool inHand)
        {
            InitializeComponent();
            InHand = inHand;
            Id = id;
            switch (id)
            {
                case 1:
                    Power = 999;
                    Name = "Dr.Balance";
                    Text = "It's okay";
                    Type = AttackType.Range;
                    break;
                case 2:
                    Power = 1;
                    Type = AttackType.Melee;
                    Name = "Хипстер";
                    Text = "";
                    break;
                case 3:
                    Power = 4;
                    Type = AttackType.Melee;
                    Name = "SMOrc";
                    Text = "Где лицо???";
                    break;
            }
        }

        private int id = 0;
        public int Power
        {
            get
            {
                return int.Parse(power.Text.ToString());
            }

            set
            {
                power.Text = value.ToString();
            }
        }

        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                selected = value;
                if (value)
                {
                    name.BackColor = Color.MediumSpringGreen;
                }
                else
                {
                    name.BackColor = Color.White;
                }
            }
        }

        public bool InHand
        {
            get
            {
                return inHand;
            }

            set
            {
                inHand = value;
            }
        }

        public new string Name
        {
            get
            {
                return name.Text;
            }

            set
            {
                name.Text = value;
            }
        }

        public new string Text
        {
            get
            {
                return text.Text;
            }

            set
            {
                text.Text = value;
            }
        }

        public AttackType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public enum AttackType
        {
            Null,
            Melee,
            Range
        }

        private AttackType type = AttackType.Null;
        private bool selected = false;

        private bool inHand = false;

        private void Card_Click(object sender, EventArgs e)
        {
            if (((sender as Control).Parent as Card).InHand)
            {
                ((sender as Control).Parent as Card).Selected = !((sender as Control).Parent as Card).Selected;
            }
        }
    }
}
