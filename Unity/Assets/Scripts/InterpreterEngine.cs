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

			//obsługa długich wyrażeń
			for(int i = 0; i < listAlternatives.Count(); i++) {
				int j = 0;
				int k = 0;
				if(listAlternatives[i].StartsWith('[')) {
					for(j = i+1; j < listAlternatives.Count(); j++) {
						if(listAlternatives[j].EndsWith(']')) {
							for(k = i+1; k <= j; k++) {
								listAlternatives[i] += listAlternatives[k];
							} // cleanup, pozbawienie klamr, reset limitera
							listAlternatives.RemoveRange((i+1), (k - i));
							listAlternatives[i].Remove(0,1);
							listAlternatives[i].Remove(listAlternatives[i].Length -1);
							k = 0;
							break;
						}
						j = 0;
					}
				}
			}

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
						return new Tuple<int, int, int, int>(-1, -1, -1, -1);
					}
					return new Tuple<int, int, int, int>(-1, -1, 0, -1);
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
			// 0	   1      2      3       4         5      6   7   8     9      10     11    12        13         14
			// [niech][każdy][każda][kelner][kelnerka][numer][do][od][stół][stołu][zmywa][poda][sprzątnie][odbierze][zamówienie]
			// słowo istnieje, można tłumaczyć polecenie
			switch(wordType) {
					// każdy każda
					case 2 :
					case 3 :
						if(listInput[i+1] == 3 || listInput[i+1] == 4) { // kelner kelnerka
							kelner = 0;
							listInput.RemoveAt(i+1); // szybszy parse, blok kolejnego warunku
						}
						break;
					// kelner kelnerka
					case 3 :
					case 4 :
						if(listInput[i+1] == 5) { // numer
							kelner = listInput[i+1];
							listInput.RemoveAt(i+1);
						}
						break;
						
					// odbierze
					case 13 :
						if(listInput[i+1] == 7 && listInput[i+2] == 9) { // od stołu
							stolik = listInput[i+3];

							if(listInput[i+4] == 14) { // zamówienie
								zadanie = 0; // odbierz zamówienie
								listInput.RemoveAt(i+4);
							}
							else { //listInput to inne słowo bądź nie istnieje
								zadanie = 2; // sprzątnij stół
							}
							listInput.RemoveRange((i+1), 3);
						}
						break;
					// poda do stołu
					case 11 :
						if(listInput[i+1] == 6 && listInput[i+2] == 9 && listInput[i+4] == 14) { // do stołu _ zamówienie
							stolik = listInput[i+3];
							zadanie = 1; // podaj zamówienie
							if(listInput[i+5] < 0) { // typ dania jest poprawny
								opcja = listInput[i+5]; // rodzaj zamówienia
								listInput.RemoveAt(i+5);
							}
							listInput.RemoveRange((i+1), 4);
						}
						break;
					// sprzątnie stół
					case 12 :
						if(listInput[i+1] == 8) {
							stolik = listInput[i+2];
							zadanie = 2;
							listInput.RemoveRange((i+1), 2);
						}
						break;
					// zmywa
					case 10 :
						zadanie = 3;
						break;
					default :
						// err coś się walnęło 
						int i = 1/0;
						// walimy mocniej
						break;
			}
			return new Tuple<int, int, int, int>(kelner, zadanie, opcja, stolik);
		}
}