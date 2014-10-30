#include <iostream>
#include <string>
#include <sstream>
#include <vector>
using namespace std;
string input;

// ustawienia kolejki
vector< vector<string> > kolejka;

int main()
{
    // input
	cout << "Interpreter komend kelnera vPierdyliard\nWpisz komendy:\n";
	while (cin >> input && (input != "quit" && input != "q")){
        stringstream ss(input);
        
        istream_iterator<string> begin(ss);
        istream_iterator<string> end;
        
        vector<string> polecenie(begin, end);
        
        kolejka.push_back(polecenie);
	}
    
    
    
    
    // quit
	cout << "stopping. no errors on runtime" << endl;
	return 0;
}
