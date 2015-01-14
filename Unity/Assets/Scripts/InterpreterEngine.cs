using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class InterpreterEngine
    {

        public List<List<string>> readFile(string filename)
        {
            StreamReader inputStream = new StreamReader(Application.dataPath + "/" + filename);
            List<List<string>> listMain = new List<List<string>>();

            while (!inputStream.EndOfStream)
            {
                string inputLine = inputStream.ReadLine();

                List<string> listAlternatives = inputLine.Split(' ').ToList<string>();
                listAlternatives.Reverse();

                /** /obsługa długich wyrażeń
                for (int i = 0; i < listAlternatives.Count(); i++)
                {
                    int j = 0;
                    int k = 0;
                    if (listAlternatives[i].StartsWith("["))
                    {
                        for (j = i + 1; j < listAlternatives.Count(); j++)
                        {
                            if (listAlternatives[j].EndsWith("]"))
                            {
                                for (k = i + 1; k <= j; k++)
                                {
                                    listAlternatives[i] += listAlternatives[k];
                                } // cleanup, pozbawienie klamr, reset limitera
                                listAlternatives.RemoveRange((i + 1), (k - i));
                                listAlternatives[i].Remove(0, 1);
                                listAlternatives[i].Remove(listAlternatives[i].Length - 1);
                                k = 0;
                                break;
                            }
                            j = 0;
                        }
                    }
                }/**/

                listMain.Add(listAlternatives);
                listAlternatives = null;
            }


            return listMain;
        }

        private int parseDictionary(string search, List<List<string>> dictionary)
        {
            int wordType = -1;
            foreach (List<string> alternatives in dictionary)
            {
                ++wordType;
                foreach (string word in alternatives)
                {
                    if (word == search)
                    {
                        return wordType;
                    }
                }
            }
            return -1;
        }

        public List<int> parseInput(string input, List<List<string>> dictionary, List<List<string>> cookbook)
        {

            List<string> listInput = new List<string>();
			List<int> listInputParsed = new List<int>();
            listInput = input.Split(' ').ToList<string>();
            listInput.Reverse();

            int kelner = -1;
            int zadanie = -1;
            int potrawa = -1;
            int stolik = -1;
			int numer = -1;

			foreach(string word in listInput) {
				string current = word;
				int temp = -1;
				// sprawdź czy to słowo, czy istnieje w słowniku, czy istnieje w potrawie, czy to liczba. jeśli nie to rzuć błąd -1 -1 -1 -1

				foreach(List<string> sublist in dictionary) {
					if(sublist.Contains(current)) {
						temp = dictionary.IndexOf(sublist);
					}
				}
				if(temp == -1) {
					foreach(List<string> sublist in cookbook) {
						if(sublist.Contains(current)) {
							potrawa = cookbook.IndexOf(sublist);
							temp = -2;
						}
					}
				}
				if(temp == -1) {
					if(int.TryParse(current, out numer)) {
						temp = -3;
					}
				}
				if(temp == -1) {
					return new List<int>(new int[] {-1, -1, -1, -1});
				}

				listInputParsed.Add(temp);
			}
			listInput.Reverse();
			listInputParsed.Reverse();
			int i = 0;
				// KELNERZY
				// każdy/a kelner/ka       numer kelnera
				// 1/2+3/4.    3/4+___.   || 3/4+5+___.
			// !!! if(listInputParsed[0] == 0) {i++;}
			if(
				(listInputParsed[i] == 1 || listInputParsed[i] == 2) &&
				(listInputParsed[i+1] == 3 || listInputParsed[i+1] == 4)
				){ // każdy kelner
				kelner = 0;
				i = 2;
			}
			else if(
				listInputParsed[i] == 3 || listInputParsed[i] == 4
				) {
				if(
					listInputParsed[i+1] == -3
				   ) { // kelner #NUMER
					kelner = int.Parse(listInput[i+1]);
					i = 2;
				}
				else if(
					listInputParsed[i+1] == 5 &&
					listInputParsed[i+2] == -3
					) { // kelner numer #NUMER
					kelner = int.Parse(listInput[i+2]);
					i = 3;
				}
				else {
					Debug.Log("ERROR : PARSER : Niejasny wybór kelnera");
					return new List<int>(new int[] {-1, -1, -1, -1});
				}
			}
				// ZADANIA

				// zmywa 10
			if(listInputParsed[i] == 10) {
				zadanie = 3;
			}
			else if(listInputParsed[i] == 13 && listInputParsed[i+1] == 14 && listInputParsed[i+2] == 7 && listInputParsed[i+3] == 9) { //odbierze zamówienie od stolika #STOLIK    13+14+7+9+___. || 13+14+7+9+5+___.
				zadanie = 0;
				if(listInputParsed[i+4] == -3) {
					stolik = int.Parse(listInput[i+4]);
				}
				else if (listInputParsed[i+4] == 5 && listInputParsed[i+5] == -3) {
					stolik = int.Parse(listInput[i+5]);
				}
				else {
					Debug.Log("ERROR : PARSER : Nie podano numeru stolika");
					//return new List<int>(new int[] {-1, -1, -1, -1});
				}
			}
			else if(listInputParsed[i] == 11) { // poda zamówienie #POTRAWA do stolika #STOLIK 11+14+___+6+9+___. || 11+14+___+6+9+5+___.
				zadanie = 1;
				if(listInputParsed[i+1] == 14 && listInputParsed[i+2] == -2) {
					if(listInputParsed[i+3] == 6 && listInputParsed[i+4] == 9) {
						if(listInputParsed[i+5] == -3) {
							stolik = int.Parse(listInput[i+5]);
						}
						else if(listInputParsed[i+5] == 5 && listInputParsed[i+6] == -3) {
							stolik = int.Parse(listInput[i+6]);
						}
						else {
							Debug.Log("ERROR : PARSER : Nie podano numeru stolika");
							//return new List<int>(new int[] {-1, -1, -1, -1});
						}
					}
					else {
						Debug.Log("ERROR : PARSER : Polecenie bez końca:zamówienie");
						//return new List<int>(new int[] {-1, -1, -1, -1});
					}
				}
				if(listInputParsed[i+1] == -2) {
					if(listInputParsed[i+2] == 6 && listInputParsed[i+3] == 9) {
						if(listInputParsed[i+4] == -3) {
							stolik = int.Parse(listInput[i+4]);
						}
						else if(listInputParsed[i+4] == 5 && listInputParsed[i+5] == -3) {
							stolik = int.Parse(listInput[i+5]);
						}
						else {
							Debug.Log("ERROR : PARSER : Nie podano numeru stolika");
							//return new List<int>(new int[] {-1, -1, -1, -1});
						}
					}
					else {
						Debug.Log("ERROR : PARSER : Polecenie bez końca:zamówienie");
						//return new List<int>(new int[] {-1, -1, -1, -1});
					}
				}
				else {
					Debug.Log("ERROR : PARSER : Nie podano potrawy");
					//return new List<int>(new int[] {-1, -1, -1, -1});
				}
			}
			else if(listInputParsed[i] == 12 && listInputParsed[i+1] == 8) { // sprzątnie stolik #STOLIK 12+8+___. || 12+8+5+___.
				zadanie = 2;
				if(listInputParsed[i+2] == -3) {
					stolik = int.Parse(listInput[i+2]);
				}
				else if(listInputParsed[i+2] == 5 && listInputParsed[i+3]) {
					stolik = int.Parse(listInput[i+3]);
				}
				else {
					Debug.Log("ERROR : PARSER : Nie podano numeru stolika");
					//return new List<int>(new int[] {-1, -1, -1, -1});
				}
			}
			else {
				Debug.Log("ERROR : PARSER : Polecenie bez końca:treść polecenia niepełna");
				//return new List<int>(new int[] {-1, -1, -1, -1});
			}
			Debug.Log("kelner " + kelner + "  zadanie " + zadanie + "  potrawa " + potrawa + "  stolik " + stolik);
			return new List<int>(new int[] {kelner, zadanie, potrawa, stolik});
            }
        }
    }
