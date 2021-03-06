
public enum Command {
	Register,
	PuzzleFinished,
    GameStart,
	Ready,
	Pause,
	QrCodeSend,
	GetGameState,
    GameStateResponse,
    GetImage,
    GetImageResponse,
	PieceScanned,
	PenaltyTimeAdd,
	AreYouThere,
    NoCommand,
    MalformedCommand,
    Registered,
    CreatePuzzle,
    PuzzleCreated,
    CreatePuzzlePart,
    GetPuzzleList,
    PuzzleList,
    GetPuzzlePartList,
    PuzzlePartList,
    SetPuzzle
}

static class CommandMethods
{
    public static string getString(this Command s1)
    {
      
        switch (s1)
        {
            case Command.SetPuzzle:
                return "SET_PUZZLE";
            case Command.GetPuzzlePartList:
                return "GET_PUZZLE_PART_LIST";
            case Command.PuzzlePartList:
                return "PUZZLE_PART_LIST";
            case Command.PuzzleList:
                return "PUZZLE_LIST";
            case Command.Register:
                return "REGISTER";
            case Command.Pause:
                return "PAUSE";
            case Command.Ready:
                return "READY";
            case Command.GetGameState:
                return "GET_GAME_STATE";
            case Command.GameStateResponse:
                return "GAME_STATE_RESPONSE";
            case Command.MalformedCommand:
                return "MALFORMED_COMMAND";
            case Command.GetImage:
                return "GET_PUZZLE_PART";
            case Command.GetImageResponse:
                return "PUZZLE_PART";
            case Command.Registered:
                return "REGISTERED";
            case Command.QrCodeSend:
                return "SHOW_QR";
            case Command.CreatePuzzle:
                return "CREATE_PUZZLE";
            case Command.CreatePuzzlePart:
                return "CREATE_PUZZLE_PART";
            case Command.GameStart:
                return "GAME_START";
            case Command.GetPuzzleList:
                return "GET_PUZZLE_LIST";
            case Command.PuzzleCreated:
                return "PUZZLE_CREATED";
            default:
                return "UNKNOWN COMMAND";
        }
    }

    public static Command getCommand(string commandString)
    {
        if (commandString.Equals("REGISTER"))
        {
            return Command.Register;
        }
        else if (commandString.Equals("SET_PUZZLE"))
        {
            return Command.SetPuzzle;
        }
        else if (commandString.Equals("GET_PUZZLE_PART_LIST")) 
        {
            return Command.GetPuzzlePartList;
        }
        else if (commandString.Equals("PUZZLE_PART_LIST")) 
        {
            return Command.PuzzlePartList;
        }
        else if (commandString.Equals("PUZZLE_LIST"))
        {
            return Command.PuzzleList;
        }
        else if (commandString.Equals("GET_PUZZLE_LIST"))
        {
            return Command.GetPuzzleList;
        }
        else if (commandString.Equals("PAUSE"))
        {
            return Command.Pause;
        } else if(commandString.Equals("READY")) 
        {
            return Command.Ready;
        }
        else if (commandString.Equals("GET_GAME_STATE"))
        {
            return Command.GetGameState;
        } 
        else if(commandString.Equals("GAME_STATE")) 
        {
            return Command.GameStateResponse;
        }
        else if (commandString.Equals("MALFORMED_COMMAND"))
        {
            return Command.MalformedCommand;
        }
        else if (commandString.Equals("REGISTERED"))
        {
            return Command.Registered;
        }
        else if (commandString.Equals("SHOW_QR"))
        {
            return Command.QrCodeSend;
        }
        else if (commandString.Equals("GET_PUZZLE_PART"))
        {
            return Command.GetImage;
        }
        else if (commandString.Equals("PUZZLE_PART"))
        {
            return Command.GetImageResponse;
        }
        else if (commandString.Equals("CREATE_PUZZLE"))
        {
            return Command.CreatePuzzle;
        }else if(commandString.Equals("PUZZLE_CREATED"))
        {
            return Command.PuzzleCreated;
        }
        else if (commandString.Equals("CREATE_PUZZLE_PART"))
        {
            return Command.CreatePuzzlePart;
        }
        else if (commandString.Equals("GAME_START"))
        {
            return Command.GameStart;
        }
        else
        {
            return Command.NoCommand;
        }
    }
}