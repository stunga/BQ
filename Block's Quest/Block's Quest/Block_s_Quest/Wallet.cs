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
        int balance;

        //Constructor
        public Wallet()
        {
            balance = 0;
        }
        
        //Add currency to wallet based on diamonds
        public void addDiamond(Diamond d)
        {
            balance += d.getValue();
        }

        //Add currency to wallet based on int value
        public void deposit(int i)
        {
            balance += i;
        }

        //Subtract currency
        public void withdraw(int i)
        {
            balance -= i;
        }

        //Gets total balance in wallet
        public int getBalance()
        {
            return balance;
        }

        //Determines if player can afford an upgrade
        public bool afford(int cost)
        {
            if (balance >= cost)
                return true;
            else
                return false;
        }

    }
}
