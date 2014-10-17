#include <iostream>

using namespace std;
char n;

int main()
{
	cout << "Interpreter komend kelnera v0.Pierdyliard\nWpisz komendy:\n";
	while (cin >> n && n != 'q'){ 
		cout << n << endl;
	}
	cout << "Hello World!" << endl;
	return 0;
}
