using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Block_s_Quest
{
    class Wallet
    {
        List<Diamond> wallet;

        //Constructor
        public Wallet()
        {
            wallet = new List<Diamond>();
        }
        
        //Add currency to wallet
        public void addDiamond(Diamond d)
        {
            wallet.Add(d);
        }

        //Gets total value of diamonds in wallet
        public int getBalance()
        {
            int temp = 0;

            //Gets the value of each diamon and adds it together
            foreach (Diamond i in wallet)
                temp += i.getValue();

            return temp;
        }

    }
}
