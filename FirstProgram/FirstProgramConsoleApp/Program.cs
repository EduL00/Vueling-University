string input = null;
bool boolean_value = false;
int integer_value = 0;
decimal decimal_value = 0.0m;
char char_value = 'a';
string string_value = "";
DateTime dateTime_value = new(2024, 12, 22);

while (true)
{
    Console.WriteLine("Introduce a boolean value (true or false)");
    input = Console.ReadLine();

    try
    {
        boolean_value = bool.Parse(input);
        break;
    }
    catch (FormatException)
    {
        Console.WriteLine("Introduce a valid value\n");
    }
}

while (true)
{

    Console.WriteLine("Now, introduce a Intreger value");
    input = Console.ReadLine();

    try
    {
        integer_value = int.Parse(input);
        break;
    }
    catch (FormatException)
    {
        Console.WriteLine("Introduce a valid value\n");
    }
}

while (true)
{

    Console.WriteLine("Now, introduce a decimal value");
    input = Console.ReadLine();

    try
    {
        decimal_value = decimal.Parse(input);
        break;
    }
    catch (FormatException)
    {
        Console.WriteLine("Introduce a valid value\n");
    }
}

while (true)
{

    Console.WriteLine("Now, introduce a character");
    input = Console.ReadLine();

    try
    {
        char_value = char.Parse(input);
        break;
    }
    catch (FormatException)
    {
        Console.WriteLine("Introduce a valid value\n");
    }
}

while (true)
{

    Console.WriteLine("Now, introduce a string");
    input = Console.ReadLine();

    try
    {
        string_value = input;
        break;
    }
    catch (FormatException)
    {
        Console.WriteLine("Introduce a valid value\n");
    }
}

while (true)
{

    Console.WriteLine("Now, introduce a Date/time (yyyy-mm-dd)");
    input = Console.ReadLine();

    try
    {
        dateTime_value = DateTime.Parse(input);
        break;
    }
    catch (FormatException)
    {
        Console.WriteLine("Introduce a valid Date/time\n");
    }
}


Console.WriteLine("Results:\n");
Console.WriteLine("The inverted value is: " + (!boolean_value) + "\n");
Console.WriteLine("The result of doing " + integer_value + " / " + decimal_value + " = " + (integer_value / decimal_value) + "\n");
Console.WriteLine(char_value + " ( " + string_value + " ) " + char_value + "\n");