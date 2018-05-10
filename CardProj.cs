using System;
//Lucas Martin
// Card Program
// C#

public class Deck
{
    private Card[] deck;
    static int bound = 52;

    // Constructor 
    public Deck()
    {
        deck = new Card[52];
        for (int i = 0; i < 52; i++) // creates 52 cards and places them into the deck array.
        {
            deck[i] = new Card();

            int toString = i % 13; // used to get value of the cards, will eventually be turned into a string
            
            if (i < 13)//first 13 iterations will create the cards in the Heart suit
            {
                deck[i].SetSuit("Heart");
                
               
            } else if (i < 26)//second 13 iterations will create cards in the Spades suit
            {
                deck[i].SetSuit("Spades");
                
            }
            else if (i < 39)//third 13 iterations will create cards in the Clubs suit
            {
                deck[i].SetSuit("Clubs");
               
            }
            else // last 13 iterations will create cards in the Diamonds suit
            {
                deck[i].SetSuit("Diamonds");
                
            }


            if(toString == 0)//if the remainder of i/13 = 0, make that particular card an Ace
            {
                deck[i].SetValue("Ace");
            }
            else if(toString == 10) //if the remainder of i/13 = 10, make card a Jack
            {
                deck[i].SetValue("Jack");
            } else if(toString == 11) // if remainder of i/13  = 11, make card a Queen
            {
                deck[i].SetValue("Queen");
            } else if(toString == 12) // if remainder of i/13 = 12, make card a King
            {
                deck[i].SetValue("King");
            } else//Otherwise, set value of card equal to remainder of i/13
            {
                deck[i].SetValue(toString.ToString());
            }

        }
    }

    
    public void Shuffle() 
        // shuffles remaining cards in deck.
        // If cards have already been pulled from top and are "on the table", 
        // they will not be shuffled
    {

        if (bound > 0)
        {
            for (int i = 0; i < bound; i++)
            {

                Random rnd = new Random(); // creates a random variable
                int randInd = rnd.Next(0, bound - 1); // makes random variable produce a number within the limits of the current deck

                //swaps the card at current iteration of the for loop with the random index.
                Card tmp = deck[i];
                deck[i] = deck[randInd];
                deck[randInd] = tmp;


            }
        } else
        {
            throw new System.Exception();

        }



    }


    //Takes Card from top of take and "places it on the table".
    public Card DealOneCard()
    {
        
        bound--; 
        // Decrement to limit the size of the arrays. Makes cards "placed on table" inaccesible. 
        
        if (bound >= 0) // if the bound is greater or equal  to zero, cards are still in the deck, so return card on top of deck
        {
                return deck[bound]; 
        } else // Otherwise deck is empty, so throw an exception
        {
            bound--;
            throw new System.Exception();
        }
    }

    public void RefillDeck() 
        // equivalent of picking up all cards on table and putting them 
        // back into the deck
    {
        if (bound != 52)
        {
            bound = 52;
        } else
        {
            
            throw new System.Exception();
        }
    }

}



public class Card
{
    private string suit;
    private string value;


    // Constructor for a single card
    public Card(){
        suit = "N/A";
        value = "N/A";
        }


    // Setter for the private variable suit
    public void SetSuit(string name)
    {
        suit = name;
    }


    // Setter for the private variable value
    public void SetValue(string v)
    {
        value = v;
    }

    //Getter for private variable suit
    public string GetSuit()
    {
        return suit;
    }

    //Getter for private variable value
    public string GetValue()
    {
        return value;
    }

}




    class CardProg
    { 

        static void Main()
        {

        Deck newDeck = new Deck(); // creates deck object

        Console.WriteLine("Welcome to the Deck of Cards program!\n\n");

        string option; // will be used to take input from the screen
        do
        {
            Console.WriteLine("What would you like to do?"); 
            // Presents User with options on how they wish to manipulate the deck
            Console.WriteLine("1.) Shuffle Deck\n2.) Remove One Card from Deck\n" +
                "3.) Remove Multiple Cards from Deck\n4.) Place Cards back into Deck\n5.) Exit Program");
            option = Console.ReadLine();


            if(option == "1") // shuffles deck
            {
                try // if deck is not empty, deck is shuffled
                {
                    newDeck.Shuffle();
                    Console.WriteLine("\nDeck has been shuffled!\n");
                }
                catch (Exception) // otherwise, catches an exception that was thrown 
                {
                    Console.WriteLine("\nNo Cards in deck! Cannot shuffle!\n");
                }
            }
            else if(option == "2") // removes single card
            {
                try // if deck is not empty, removes card from deck and prints what card has been removed
                {
                   
                    Card lastCard = newDeck.DealOneCard();
                    Console.WriteLine("\nThe {0} of {1} has been removed from the deck!\n", lastCard.GetValue(), lastCard.GetSuit());
                } catch(Exception)//Otherwise, catches an exception that was thrown 
                {
                    Console.WriteLine("\nAll cards have been dealt!\n");
                }

            }
            else if(option == "3")// removes multiple cards, if  user asks to remove more cards then there are in the deck, 
                                  //it will remove all cards in deck then print a message saying all cards have been removed
            {  
                    Console.WriteLine("\nHow many cards would you like to remove?");

                    string secondOp = Console.ReadLine();

                if (int.TryParse(secondOp, out int num)) 
                // Conditional check to make sure user inputted a integer and not an error-inducing variable such as letters
                {
                    if (num == 0)//if they wish to remove 0 cards, do not remove any
                    {
                        Console.WriteLine("\nNo cards were removed!\n");
                    }
                    else if (num > 0)//otherwise remove amount of cards they wish for you to remove
                    {

                        try // continues removing cards, unless runs out of cards in the deck
                        {
                            Console.WriteLine();
                            for (int i = 0; i < num; i++)
                            {
                                Card lastCard = newDeck.DealOneCard();
                                Console.WriteLine("The {0} of {1} has been removed from the deck!", lastCard.GetValue(), lastCard.GetSuit());
                            }
                            Console.WriteLine();
                        }
                        catch (Exception) // If no more cards in deck, catch exception that is thrown
                        {
                            Console.WriteLine("All cards have been dealt!\n");
                        }
                    }
                    else // Input validation to prevent error from a negative number
                    {
                        Console.WriteLine("\nCan not use negative numbers!\n");
                    }

                }
                else // if user entered invalid symbol such as letters 
                {
                    Console.WriteLine("\nInvalid Input!\n");
                }
                   
                
            } else if (option == "4") // refills deck
            {
                try // if deck is not empty, refill the deck
                {
                    newDeck.RefillDeck(); 

                    Console.WriteLine("\nDeck has been refilled!\n");
                }
                catch (Exception)// Otherwise, catch the thrown exception
                {
                    Console.WriteLine("\nDeck is already full!\n");
                }
            }
            else if(option == "5") // exits program
            {
                Console.WriteLine("\nGoodbye!\n");
            }
            else//Input validation to make sure user is inputting proper string
            {
                Console.WriteLine("\nInvalid Input!\n");
            }


        } while (option!="5");
       
    }
    }

