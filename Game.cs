using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeUML
{
    public class Game
    {
        public Field Field { get; set; }
        public GameEffect[] gameEffects;
        public uint NextTurnInterval { get; set; } = 500;

        GameChecker gameChecker;
        public Game()
        {
            gameChecker = new GameChecker(this);
            Initialize();
        }
        public void Initialize()
        {
            Field = new Field(20, 10);

            gameEffects = new GameEffect[0];

        }



        public void Run()
        {
            SoundOutput.Start();
            GameSpeedEffect previosIterationSpeedEffect = null;

            Output.Render(Field, null);

            while (true)
            {

                ConsoleKeyInfo key = new ConsoleKeyInfo();

                if (Input.KeyAvaible())
                {
                    key = Input.ReadKey();

                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        Point savedDirection = Field.Snake.Direction;
                        Field.Snake.Direction = new Point(0, -1);

                        if (gameChecker.CheckForGoingOnHimself())
                            Field.Snake.Direction = savedDirection;
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        Point savedDirection = Field.Snake.Direction;
                        Field.Snake.Direction = new Point(0, 1);

                        if (gameChecker.CheckForGoingOnHimself())
                            Field.Snake.Direction = savedDirection;
                    }
                    else if (key.Key == ConsoleKey.LeftArrow)
                    {
                        Point savedDirection = Field.Snake.Direction;
                        Field.Snake.Direction = new Point(-1, 0);

                        if (gameChecker.CheckForGoingOnHimself())
                            Field.Snake.Direction = savedDirection;
                    }
                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        Point savedDirection = Field.Snake.Direction;
                        Field.Snake.Direction = new Point(1, 0);

                        if (gameChecker.CheckForGoingOnHimself())
                            Field.Snake.Direction = savedDirection;

                    }
                }



                if (TryMove() == true)
                    Move();
                // todo Розкоментуйте! щоб гра закінчувалвсь!!!
                // else
                //    return;

                GameVisualEffect visualEffect = null;
                GameSpeedEffect gameSpeedEffect = null;

                for (int i = 0; i < gameEffects.Length; i++)
                {
                    if (gameEffects[i] is GameVisualEffect && !((GameVisualEffect)gameEffects[i]).TimeIsOver())
                        visualEffect = (GameVisualEffect)gameEffects[i];
                    else if (gameEffects[i] is GameSpeedEffect && !((GameSpeedEffect)gameEffects[i]).TimeIsOver())
                        gameSpeedEffect = (GameSpeedEffect)(gameEffects[i]);
                }

                Output.Render(Field, visualEffect);

                if (gameSpeedEffect != previosIterationSpeedEffect && gameSpeedEffect == null)
                    SoundOutput.Normal();
                else if (gameSpeedEffect != previosIterationSpeedEffect && gameSpeedEffect != null &&  ((GameSpeedEffect)gameSpeedEffect).SpeedRatio > 1)
                    SoundOutput.Fast();

                // delete ended Effects
                for (int i = 0; i < this.gameEffects.Length; i++)
                {
                    gameEffects = gameEffects.Where(eff => !eff.TimeIsOver()).ToArray();
                }

                previosIterationSpeedEffect = gameSpeedEffect;

                SoundOutput.CheckLoopPlay();

                Thread.Sleep((int)(this.NextTurnInterval / (gameSpeedEffect != null ? gameSpeedEffect.SpeedRatio : 1)));

            }

        }

        private bool TryMove()
        {
            return !gameChecker.CheckNextMoveForBarrier();
        }

        private void Move()
        {
            if (gameChecker.CheckNextMoveForBonus())
            {
                Point nextHeadPosition = Field.Snake.Head;
                nextHeadPosition.Offset(Field.Snake.Direction);

                // Eat BONUS
                for (int i = 0; i < Field.Bonuses.Length; i++)
                {
                    if (Field.Bonuses[i].Position == nextHeadPosition)
                    {
                        Bonus bonus = Field.Bonuses[i];

                        if (bonus is Apple)
                        {
                            Apple apple = (Apple)bonus;
                            Point[] body = Field.Snake.Body;

                            Point newHead = Field.Snake.Head;
                            newHead.Offset(Field.Snake.Direction);

                            body = body.Prepend(newHead).ToArray();
                            Field.Snake.Body = body;


                            Field.DeleteBonus(i);
                            Field.AddApple();

                        }
                        else if (bonus is Speed)
                        {
                            Speed speed = (Speed)bonus;

                            Point[] body = Field.Snake.Body;

                            Point newHead = Field.Snake.Head;
                            newHead.Offset(Field.Snake.Direction);

                            body = body.Prepend(newHead).ToArray();
                            Array.Resize(ref body, body.Length - 1);

                            Field.Snake.Body = body;

                            Field.DeleteBonus(i);
                            Field.AddSpeed();

                            AddGameEffectSpeed();
                        }
                        else if (bonus is BonusGameEffectVisual)
                        {
                            Point[] body = Field.Snake.Body;

                            Point newHead = Field.Snake.Head;
                            newHead.Offset(Field.Snake.Direction);

                            body = body.Prepend(newHead).ToArray();
                            Array.Resize(ref body, body.Length - 1);

                            Field.Snake.Body = body;

                            Field.DeleteBonus(i);
                            Field.AddBonusGameEffectVisual();

                            AddGameEffectVisual();
                        }
                        else
                            throw new Exception("known Bonus type is Apple, Speed");
                    }
                }


            }
            else if (gameChecker.CheckNextMoveForSpecialItem())
            {
                Point nextHeadPosition = Field.Snake.Head;
                nextHeadPosition.Offset(Field.Snake.Direction);

                for (int i = 0; i < Field.SpecialItems.Length; i++)
                {
                    SpecialItem item = Field.SpecialItems[i];

                    if (item is DualHole)
                    {
                        DualHole dualHole = (DualHole)item;

                        if (nextHeadPosition == dualHole.Position1)
                        {
                            nextHeadPosition = dualHole.Position2;
                            nextHeadPosition.Offset(Field.Snake.Direction);

                            Point[] body = Field.Snake.Body;
                            body = body.Prepend(nextHeadPosition).ToArray();

                            Array.Resize(ref body, body.Length - 1);
                            Field.Snake.Body = body;
                        }
                        else if (nextHeadPosition == dualHole.Position2)
                        {
                            nextHeadPosition = dualHole.Position1;
                            nextHeadPosition.Offset(Field.Snake.Direction);

                            Point[] body = Field.Snake.Body;
                            body = body.Prepend(nextHeadPosition).ToArray();

                            Array.Resize(ref body, body.Length - 1);
                            Field.Snake.Body = body;
                        }

                    }
                    else
                        throw new Exception(" known SpecialItem is DualHole");
                }
            }
            else
            {
                Point nextHeadPosition = Field.Snake.Head;
                nextHeadPosition.Offset(Field.Snake.Direction);

                Point[] body = Field.Snake.Body;

                body = body.Prepend(nextHeadPosition).ToArray();
                Array.Resize(ref body, body.Length - 1);

                Field.Snake.Body = body;
            }
        }

        public void AddGameEffectVisual()
        {
            Array.Resize(ref gameEffects, gameEffects.Length + 1);
            gameEffects[gameEffects.Length - 1] = new GameVisualEffect(DateTime.Now, 4000);
        }

        public void AddGameEffectSpeed()
        {
            GameEffect[] gameEffects = this.gameEffects;

            Array.Resize(ref gameEffects, gameEffects.Length + 1);
            gameEffects[gameEffects.Length - 1] = new GameSpeedEffect(DateTime.Now, 9000, 3.0d);

            this.gameEffects = gameEffects;
        }

        private class GameChecker
        {
            Game game;
            public GameChecker(Game game)
            {
                this.game = game;
            }

            public bool CheckNextMoveForBonus()
            {
                Point snakeHead = game.Field.Snake.Head;
                Point nextHeadPosition = snakeHead;
                nextHeadPosition.Offset(game.Field.Snake.Direction);

                for (int i = 0; i < game.Field.Bonuses.Length; i++)
                {
                    Bonus bonus = game.Field.Bonuses[i];

                    if (bonus.Position == nextHeadPosition)
                        return true;
                }

                return false;
            }
            public bool CheckNextMoveForSpecialItem()
            {
                Point snakeHead = game.Field.Snake.Head;
                Point nextHeadPosition = snakeHead;
                nextHeadPosition.Offset(game.Field.Snake.Direction);

                for (int i = 0; i < game.Field.SpecialItems.Length; i++)
                {
                    if (!(game.Field.SpecialItems[i] is DualHole))
                        throw new Exception("only DualHole Special item is known");

                    DualHole dhole = (DualHole)game.Field.SpecialItems[i];

                    if (nextHeadPosition == dhole.Position1 || nextHeadPosition == dhole.Position2)
                        return true;
                }



                return false;
            }
            public bool CheckNextMoveForBarrier()
            {

                if (CheckForBoundaries())
                    return true;


                if (CheckForSelfEating())
                    return true;


                if (CheckForGoingOnHimself())
                    return true;


                return false;
            }

            public bool CheckForBoundaries()
            {
                Point snakeHead = game.Field.Snake.Head;
                Point nextHeadPosition = snakeHead;
                nextHeadPosition.Offset(game.Field.Snake.Direction);


                for (int i = 0; i < game.Field.SpecialItems.Length; i++)
                {
                    if (!(game.Field.SpecialItems[i] is DualHole))
                        throw new Exception("only DualHole Special item is known");

                    DualHole dhole = (DualHole)game.Field.SpecialItems[i];
                    if (nextHeadPosition == dhole.Position1)
                    {
                        nextHeadPosition = dhole.Position2;
                        nextHeadPosition.Offset(game.Field.Snake.Direction);
                    }
                    else if (nextHeadPosition == dhole.Position2)
                    {
                        nextHeadPosition = dhole.Position1;
                        nextHeadPosition.Offset(game.Field.Snake.Direction);
                    }
                }


                Rectangle fieldRect = new Rectangle()
                {
                    X = 0,
                    Y = 0,
                    Width = game.Field.Width,
                    Height = game.Field.Height
                };

                if (!fieldRect.Contains(nextHeadPosition))
                    return true;

                return false;
            }
            public bool CheckForSelfEating()
            {
                Point newHead = game.Field.Snake.Head;
                newHead.Offset(game.Field.Snake.Direction);

                for (int i = 0; i < game.Field.Snake.Body.Length; i++)
                {
                    if (game.Field.Snake.Body[i] == newHead)
                        return true;
                }

                return false;
            }
            public bool CheckForGoingOnHimself()
            {
                Point newHead = game.Field.Snake.Head;
                newHead.Offset(game.Field.Snake.Direction);

                if (newHead == game.Field.Snake.Body[1])
                    return true;

                return false;
            }
        }
    }


}