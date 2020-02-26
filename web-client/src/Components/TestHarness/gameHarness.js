import React from 'react'
import GameService from '../../Services/GameService'

class GameHarness extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
          gameDefinition: this.props.gameDefinition,
          isActive: this.props.isActive,
          collectableItems: this.props.collectableItems
        }
    }

   
    start = () => {
      console.log("Starting Game")
       GameService.startGame(this.props.gameId).then((data) => {
         console.log("Game started: ", data)
         this.setState({
          isActive: true
        })
       })
    }

    stop = () => {
      console.log("Stopping Game")
       GameService.stopGame(this.props.gameId).then((data) => {
         console.log("Game stopped: ", data)
         this.setState({
           isActive: false
         })
       })
    }

    reset = () => {
      console.log("Removing teams collected items (well, collected items)")
       GameService.resetCollectedItems(this.props.gameId).then((data) => {
         console.log("items removed", data)
         console.log(data)
       })
    }

    get = () => {
      console.log("Getting game")
       GameService.get(this.props.gameId).then((data) => {
        console.log("Game data loaded:", data)
         this.setState({
           isActive: data.IsActive,
           collectableItems: data.CollectableItems
        })
       })
    }

    render() {
      return (
            <div>
              <h2>Game ({this.props.gameId})</h2>
              <div className="game">
              <div>Game Status: {this.state.isActive ? "Active" : "Stopped"}</div>
              <div>Collectable Items {this.state.collectableItems.length}
                <ol>
                  {this.state.collectableItems.map((item, i) => 
                    <li key={i}>{item.ExternalId} - {item.Name}</li>
                  ) }
                </ol>
              </div>

              <button onClick={this.start}>Start Game</button>
              <button onClick={this.stop}>Stop Game</button>
              <button onClick={this.reset}>Remove All Teams Collected Items</button>
            </div>
            </div>
      );
    }
  }

  export default GameHarness
  