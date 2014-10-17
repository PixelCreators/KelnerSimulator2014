#include <iostream>
#include <string.h>

using namespace std;
string input;

int main()
{
	cout << "Interpreter komend kelnera v0.Pierdyliard\nWpisz komendy:\n";
	while (cin >> input && input != "quit"){ 
		cout << input << endl;
	}
	cout << "Hello World!" << endl;
	return 0;
}
