# PaoloAlto_PermutationService


Searching for permutations of a user input word in a local DB file. 


1. The algorithm builds a dictionary from the input word where the key is a character
and the value is the count of this letter, in the complete word.

2. Before starting the search, the input word is convereted to lower-case.







<b>INSTALLATION OF MONO</b>

1st step, Adding the repo, paragraph 1:

https://www.mono-project.com/download/stable/#download-lin
I used Ubuntu 20.04

2nd step, install Mono, paragraph 2:

https://www.mono-project.com/download/stable/#download-lin


<b>Running the web-service:</b>

In terminal: cd to the project files folder (not the .sln file) and run:
xsp4 --port 8000


for example: "Projects/Permutation_Services/Permutation_Services"


once there, run: "xsp4 --port 8000"



open a web browser using and paste the address: 

http://127.0.0.1:8000 OR http://localhost:8000

--or

http://127.0.0.1:8000/api/v1/similar.asmx?page=op&op=CheckWordAsync&bnd=similarSoap12&tab=test

http://127.0.0.1:8000/api/v1/similar.asmx

http://127.0.0.1:8000/api/v1/stats.asmx


<b><h2>Press "Back" after viewing the results.</h2></b>










