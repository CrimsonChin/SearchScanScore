import React from 'react'
import GameService from '../../Services/GameService'
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

class GameHarness extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
          message: "",
          gameDefinition: this.props.gameDefinition,
          isActive: this.props.isActive,
          collectableItems: this.props.collectableItems
        }
    }

    componentDidMount = () => {
      const hubConnection = new HubConnectionBuilder()
      .withUrl("https://localhost:44394/chatHub")
      .configureLogging(LogLevel.Trace)
      .build()
  
    
      this.setState({ hubConnection: hubConnection }, () => {
        this.state.hubConnection
          .start()
          .then(() => console.log('Connection started!'))
          .catch(err => console.log('Error while establishing connection :(', err));
      })
    }

    sendMessage = (e) => {
      this.state.hubConnection.invoke("SendMessage", this.state.message)
      .catch(function (err) {
          return console.error(err.toString())
      });
      e.preventDefault()
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
      console.log("Reset")
       GameService.reset(this.props.gameId).then((data) => {
         console.log(data)
         window.location = window.location;
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

    handleChange = (event) => {
      this.setState({message: event.target.value});
    }

    render() {
      return (
            <div>
              <h2>Game ({this.props.gameId})</h2>
              <div className="game">
              <div>Game Status: {this.state.isActive ? "Active" : "Stopped"}</div>
              <div><input type="text" value={this.state.message} onChange={this.handleChange}></input><button onClick={this.sendMessage}>Send</button></div>
              <div>Collectable Items {this.state.collectableItems.length}
                <ol>
                  {this.state.collectableItems.map((item, i) => 
                    <li key={i}>{item.ExternalId} - {item.Name}</li>
                  ) }
                </ol>
              </div>

              <button onClick={this.start}>Start Game</button>
              <button onClick={this.stop}>Stop Game</button>
              <button onClick={this.reset}>Reset</button>
            </div>
            </div>
      );
    }
  }

  export default GameHarness
  