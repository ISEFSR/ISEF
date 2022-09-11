Data procerssing library DPL
===================================================================================
NOTE!

Toto ma byt nahrada namiesto WEB API
Jednduchsie / rychlejsie by samozjreme bolo spravit backend cez webapi
Frontend ako ASP / WPF, ale v pripade ak pride niekdo starsi ( co je standard v CVTI tak to nebude vediet upravit )

===================================================================================

CRUD nad tabulkami na MSSQL serveri

V podstate to nahradza ORMko ktore by som tu za normalnych okolnosti pouzil

Root teda cvti.data obsahuje vacsinou finalne managery

Views/ obsahuje riadky / tabulky nad VIEWS

Tables/ obsahuje riadky z tabuliek ( ciselnikov a dat )

Output/ obsahuje classy zodpovedne za export dat 

Input/ obsahuje classy zodpovedne za nacitanie udajov zo vstupnych suborv

Functions/ obsahuje pouzitelne funkcie, ktore sa daju aplikovat na stlpce

Exceptions/ obsahuje vsetky vynimky pre DPL

Enums/ obsahuje vsetky enumeracie pre DPL

Core/ obsahuje mostyl abstraktne classy a interfaces

Conditions/ obsahuje implementacie podmienok vyberu

Columns/ obsaghuje vyberove stlpce (AssuViewcolmn zatial nemam iny)