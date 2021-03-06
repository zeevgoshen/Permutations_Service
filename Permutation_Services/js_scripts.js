            function QueryPermutationDB(){
            
                var url = "http://127.0.0.1:8000/api/v1/similar.asmx?page=op&tab=test&op=Find_Permutations_In_DB&bnd=similarSoap12&ext=testform&word=";
                var new_value = document.getElementById("first_word").value;
            
                if (new_value == "") {
                    alert("Word field is empty.");
                    return;
                }
                var new_url = updateQueryStringParameter(url,"word",new_value);
                document.getElementById("link_words").href = new_url;
            }
            
            function OpenStats(){
                url = "http://127.0.0.1:8000/api/v1/stats.asmx?page=op&tab=test&op=Show_Stats&bnd=statsSoap12&ext=testform";
                document.getElementById("statsLink").href = url;
            }
                       
            function updateQueryStringParameter(uri, key, value) {           
              var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
              var separator = uri.indexOf('?') !== -1 ? "&" : "?";
              if (uri.match(re)) {
                return uri.replace(re, '$1' + key + "=" + value + '$2');
              }
              else {
                return uri + separator + key + "=" + value;
              }
            }
