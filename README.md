Coding Conventions:

# Classes

PascalCase -> public class SomeClassName

# Variables

var x = 5;

var some_snake_case_variable_name = "some string value"; // <-- try to communicate as much intent as possible

# Assets

If you need to push assets to the repo, make a separate branch and a pull request. 
It would also be beneficial to document what the assets are for. 
Make sure they follow the naming convention *below* so that they can be organized in a concise way.

'Namespace' the asset name with what it is associated with. For example, if we're creating an animation file, we'd prepend the Player_ to it 
followed by what the file is intended to be doing.
GameObjects --> Player_Camera
            --> Player_Model
            --> Monster_Spawner
            --> Monster_SpawnerModel

That's pretty much it. Otherwise, try to communicate as much intent as possible when coding.
