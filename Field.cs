using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeUML
{
    public class Field
    {
        private Bonus[] bonuses;
        private SpecialItem[] specialItems;
        private Snake snake;

        readonly int width;
        readonly int height;
        public int Width 
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }
        public Bonus[] Bonuses
        {
            get { return  bonuses; }
        }

        public SpecialItem[] SpecialItems
        {
            get { return specialItems; }
        }
        public Snake Snake
        {
            get { return snake; }
        }


        static FieldRandomizer fieldRandomizer;
        BonusFabric bonusFabric;
        SpecialItemFabric specialItemFabric;
        


        public void Initialize()
        {
            snake = new Snake();

            bonuses = new Bonus[0];
            specialItems = new SpecialItem[0];


            AddApple();
            AddSpeed();
            AddApple();

            AddBonusGameEffectVisual();

            AddDualHole();

        }

        public Field(int width, int height)
        {
            this.width = width;
            this.height = height;

            fieldRandomizer = new FieldRandomizer();
            bonusFabric = new BonusFabric();
            specialItemFabric = new SpecialItemFabric();

            Initialize();
        }

        public void DeleteBonus(int i)
        {
            bonuses[i] = null;

            bonuses = bonuses.Where(b => b != null).ToArray();
        }

        public void DelteSpecialItem(int i)
        {
            specialItems[i] = null; 

            specialItems = specialItems.Where(b => b != null).ToArray();
        }

        public void AddApple()
        {
            Array.Resize(ref bonuses, bonuses.Length + 1);
            bonuses[bonuses.Length - 1] = bonusFabric.GenerateApple(this, fieldRandomizer);
        }

        public void AddSpeed()
        {
            Array.Resize(ref bonuses, bonuses.Length + 1);
            bonuses[bonuses.Length - 1] = bonusFabric.GenerateSpeed(this, fieldRandomizer);
        }

        public void AddDualHole()
        {
            Array.Resize(ref specialItems, specialItems.Length + 1);
            specialItems[specialItems.Length - 1] = specialItemFabric.GenerateDualHole(this, fieldRandomizer);
        }

        public void AddBonusGameEffectVisual()
        {
            Array.Resize(ref bonuses, bonuses.Length + 1);
            bonuses[bonuses.Length - 1] = bonusFabric.GenerateBonusGameEffectVisual(this, fieldRandomizer);
        }
    }
}