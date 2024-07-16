# Pathfinding Project

Pathfinding Project is a university project developed as part of the AI course at Gamagora. This project simulates pathfinding in a top-down point & click tile movement game, inspired by Civilization 5. Each tile has a specific cost (hill, ground, tree), and users can adjust these settings. The game features both Dijkstra's and A* algorithms for shortest path computation, with visual feedback for the pathfinding process.

## Preview

https://github.com/user-attachments/assets/7aa8686f-72cd-4551-9de9-640c11be043a


## Installation

To set up the project, follow these steps:

1. Clone the repository:
    
    ```bash
    git clone https://github.com/yourusername/pathfinding_project.git
    ```

2. Ensure you have Unity version 2021.3.40f1 or later installed.
3. Open the project in Unity.

## Usage

Once the project is opened in Unity:

1. Open the Scenes folder and load the main scene.
2. Press the Play button in the Unity editor to run the simulation.
3. Click on any tile to see the fox move to that tile.
   - **Left Click**: Uses Dijkstra's algorithm for shortest path computation.
   - **Right Click**: Uses A* algorithm for shortest path computation.
4. Adjust tile costs in the Inspector to see how different settings affect the pathfinding behavior.

## Features

- **Pathfinding Algorithms**: Implements both Dijkstra's and A* algorithms to simulate pathfinding in a tile-based environment.
- **Customizable Tile Costs**: Adjust various tile costs (hill, ground, tree) to change the pathfinding behavior.
- **Real-time Interaction**: Observe how the fox finds the path in real-time with visual feedback.

## Visual Feedback

- **Dijkstra's Algorithm**:
  - **Green Tiles**: Represent the start and end goal tiles.
  - **Blue Tiles**: Represent the shortest path.
  - **Yellow Tiles**: Represent all the neighbors that were consulted.

- **A* Algorithm**:
  - **Green Tiles**: Represent the start and end goal tiles.
  - **Blue Tiles**: Represent the shortest path.
  - **Purple Tiles**: Represent all the neighbors that were consulted.

## Authors

- **Wassil Lebsaira**

## Contributing

Contributions are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests and documentation as appropriate.

## License

This project is licensed under the [MIT License](https://choosealicense.com/licenses/mit/).

## Acknowledgments

- Inspiration: https://www.youtube.com/watch?v=jr5Gs1ZS_d4
- Assets: https://www.kenney.nl/assets/hexagon-kit (Tiles)

---
Feel free to contact me for any questions or suggestions regarding the project. Enjoy exploring the pathfinding algorithms!



