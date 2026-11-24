using UnityEngine;

public class TestOutCodeScript : MonoBehaviour
{
    void Start()
    {
        print("_-_-_-OutcodeStart-_-_-_");
        
        //Constructors
        OutCode o1 = new OutCode(new Vector2(2f, 2f));     
        print("o1 (2,2): " + o1 + " Expected: 1001");

        OutCode o2 = new OutCode(new Vector2(-2f, -2f));   
        print("o2 (-2,-2): " + o2 + " Expected: 0110");

        OutCode o3 = new OutCode(new Vector2(2f, -2f));   
        print("o3 (2,-2): " + o3 + " Expected: 0101");

        OutCode o4 = new OutCode(new Vector2(-2f, 2f));    
        print("o4 (-2,2): " + o4 + " Expected: 1010");

        OutCode o5 = new OutCode(new Vector2(0.2f, 0.2f)); 
        print("o5 (0.2,0.2): " + o5 + " Expected: 0000");

        OutCode o6 = new OutCode();                        
        print("o6 empty: " + o6 + " Expected: 0000");

        OutCode o7 = new OutCode(o1);                      //copy of o1
        print("o7 copy of o1: " + o7 + " Expected: 1001");

        OutCode o8 = new OutCode(true, false, true, false); 
        print("o8 bools: " + o8 + " Expected: 1010");

        //Logical OR
        print("o2 + o3 = " + (o2 + o3)+ " Expected: 0111"); 
        print("o3 + o3 = " + (o3 + o3) + " Expected: 0101"); 

        //Logical AND
        print("o1 * o2 = " + (o1 * o2) + " Expected: 0000"); 
        print("o1 * o3 = " + (o1 * o3) + " Expected: 0001"); 
        print("o3 * o3 = " + (o3 * o3) + " Expected: 0101"); 

        //Equality
        print("o5 == o6: " + (o5 == o6) + " Expected: true"); 
        print("o1 == o7: " + (o1 == o7) + " Expected: true"); 
        print("o1 == o2: " + (o1 == o2) + " Expected: false"); 

        //Not equal
        print("o1 != o2: " + (o1 != o2) + " Expected: true"); 
        print("o4 != o8: " + (o4 != o8) + " Expected: false"); 
        
        print("_-_-_-OutcodeEnd-_-_-_");
    }
}