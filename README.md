# PaoloAlto_PermutationService


Searching for permutations of a user input word in a local DB file. 


<b>Permutation definition:</b> a string is a permutation of some characters, if all string characters are contained in the list of the permutation characters.


Accept a string and store the characters as permutation character array A.

The length L of the array gives the length of the entries to match in the dictionary.

Read the dictionary into a hash table D for all entries E of D

Print only those Es of length L an where all chars of E are contained in A

	
* Before starting the search, the input word is convereted to lower-case.


<b>*KNOWN ISSUES</b>

-No UI updates on long words being permutated.





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

<b>In case of any issues/error messages, run:</b>
"sudo lsof -i:8000 -n -P" in terminal - to check if something is using port 8000
(if so then kill it and re-run "xsp4 --port 8000").


<b>* Don't forget to build the source code after cloning/downloading it.</b>


open a web browser and paste the address: 

http://127.0.0.1:8000 OR http://localhost:8000

--or

http://127.0.0.1:8000/api/v1/similar.asmx?page=op&op=CheckWordAsync&bnd=similarSoap12&tab=test

http://127.0.0.1:8000/api/v1/similar.asmx

http://127.0.0.1:8000/api/v1/stats.asmx


<b><h2>Press "Back" after viewing the results.</h2></b>










