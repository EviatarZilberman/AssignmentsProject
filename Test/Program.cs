using Test;
using Utilities;

Person[] p = new Person[6];
p[0] = new Person("a");
p[1] = new Person("b");
p[2] = new Person("c");
p[3] = new Person("d");
p[4] = new Person("e");
p[5] = new Person("rrr");

/*for (int i = 0; i < p.Length - 1; i++)
{
    DBManager<Person>.Instance("People").Insert("test", p[i]);
}*/
//DBManager<Person>.Instance("People").upsertRecord<Person>("test", p[1]._id, p[5]);