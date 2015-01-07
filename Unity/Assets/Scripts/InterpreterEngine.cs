using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class InterpreterEngine {

	public List<List<string>> readFile(string filename) {
		StreamReader inputStream = new StreamReader(Application.dataPath+"/" + filename);
		List<List<string>> listMain = new List<List<string>>();

		while(!inputStream.EndOfStream) {
			string inputLine = inputStream.ReadLine();

			List<string> listAlternatives = inputLine.Split(' ').ToList<string>();
			listAlternatives.Reverse();

			listMain.Add(listAlternatives);
			listAlternatives = null;
		}

		return listMain;
	}

	int parseDictionary(string search, List<List<string>> dictionary) {
		int wordType = -1;
		foreach(List<string> alternatives in dictionary) {
			++wordType;
			foreach(string word in dictionary) {
				if(word == search) {
					return wordType;
				}
			}
		}
		return wordType;
	}

	 public static Tuple<int, int, int, int> parseInput(string input, List<List<string>> dictionary, List<List<string>> cookbook) {

		List<string> listInput = new List<string>();
		listInput = input.Split(' ').ToList<string>();
		listInput.Reverse();

		List<List<string>> tmpDictionary = new List<List<string>>(dictionary);
		List<List<string>> tmpCookbook = new List<List<string>>(cookbook);

		int kelner = -1;
		int zadanie = -1;
		int opcja = -1;
		int stolik = -1;

		for(int i = 0; i < listInput.Count(); i++) {
			string tmpWord = listInput[i];
			int tmpNumber = -1;

			int wordType = parseDictionary(tmp, tmpDictionary);
			if(wordType == -1) {// error NaN - nie ma takiego słowa polecenia
				wordType = parseDictionary(tmp, tmpCookbook);
				if(wordType == -1) {
					//error NaN - nie ma takiej potrawy
					if(!int.Parse(tmpWord) {
						//error NaN - to nie liczba, syntax error ;D
					}
				}
				else {
					// można bezpiecznie usunąć potrawę z tmpCookbook
					tmpCookbook.RemoveAt(wordType);
					wordType *= -1;
				}
			}
			else {
				// można bezpiecznie usunąć słowo z tmpDictionary
				tmpDictionary.RemoveAt(wordType);
			}
			// słowo istnieje, można tłumaczyć polecenie
			switch(wordType) {
				default: // ustawianie potrawy
						if(wordType < 0) {
							opcja = wordType * -1;
						}
						break;
				case 2: // kelner
						if(int.TParse(listInput[i+1], tmpNumber) == 1) { // dany kelner numer
							kelner = tmpNumber;
						}
						else if(i == 1) { // wszyscy kelnerzy
							kelner = 0;
						}
						// else kelner lookup failed ;D
						break;

				// dodam więcej caseów jak dostanę słownik
			}
			return new Tuple<int, int, int, int>(kelner, zadanie, opcja, stolik);
		}
}