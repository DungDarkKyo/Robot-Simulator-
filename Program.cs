using RobotSimulator;

internal class Program
{
    private const string MOVE = "MOVE";
    private const string LEFT = "LEFT";
    private const string RIGHT = "RIGHT";
    private const string REPORT = "REPORT";
    private const string PLACE = "PLACE";

    private static void Main(string[] args)
    {
        Robot robot = new();

        while (true)
        {
            Console.Write("Enter command: ");
            string command = Console.ReadLine();
            if (HandleCommand(robot, command))
            {
                if (command == REPORT)
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("Invalid command.");
            }
        }

        Console.ReadLine();
    }

    private static bool HandleCommand(Robot robot, string command)
    {
        if (string.IsNullOrEmpty(command))
        {
            return false;
        }

        command = command.Trim().ToUpper();

        if (command.StartsWith(PLACE))
        {
            string[] args = command.Split(' ')[1].Split(',');
            if (args.Length != 3)
            {
                return false;
            }

            int x, y;
            if (!int.TryParse(args[0], out x) || !int.TryParse(args[1], out y))
            {
                return false;
            }

            if (string.IsNullOrEmpty((string)args[2]))
            {
                return false;
            }

            return robot.Placing(x, y, args[2]);
        }
        if (command == MOVE)
        {
            return robot.Move();
        }
        if (command == LEFT)
        {
            return robot.Left();
        }
        if (command == RIGHT)
        {
            return robot.Right();
        }
        if (command == REPORT)
        {
            Console.WriteLine(robot.ToString());
            return true;
        }
        return false;
    }
}