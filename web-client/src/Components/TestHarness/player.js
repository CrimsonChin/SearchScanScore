import React from 'react'
import TeamService from '../../Services/TeamService'
import Tabs from '../Tabs/tabs';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

require('../Tabs/styles.css');

class Player extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
          hubConnection: null,
          collectableItemId: "",
          sightings: [],
          collectedItems: [],
          remainingItems: [],
        }
    }

    handleChange = (event) => {
      this.setState({collectableItemId: event.target.value});
    }

    collect = (event) => {
      console.log("Collecting Item: ", this.state.collectableItemId)
      TeamService.collectItem(this.props.gameId, this.props.teamId, this.state.collectableItemId).then((data) => {
        console.log(data)
        this.setState({
          collectableItemId: ""
        })
      })
    }

    get = (event) => {
      console.log("Team -> Get")
      TeamService.get(this.props.gameId, this.props.teamId).then((data) => {
        console.log(data)
        data = data || {
          ItemsCollected: [],
          Sightings: [],
          RemainingItems: []
        }
        this.setState({
          collectedItems: data.ItemsCollected,
          remainingItems: data.RemainingItems,
          sightings: data.Sightings
        })
      })
    }

    componentDidMount = () => {
      this.get();

      const hubConnection = new HubConnectionBuilder()
      .withUrl("https://localhost:44394/chatHub")
      .configureLogging(LogLevel.Trace)
      .build()
  
    
      this.setState({ hubConnection }, () => {
        this.state.hubConnection
          .start()
          .then(() => console.log('Connection started!'))
          .catch(err => console.log('Error while establishing connection :('))
          
          this.state.hubConnection.on('AdminMessage', (message) => {
            const text = `ADMIN MESSAGE: ${message}`;
            
            this.setState({ 
              message: text
             });
          });
      })

      const teamHubConnection = new HubConnectionBuilder()
      .withUrl("https://localhost:44394/teamHub")
      .configureLogging(LogLevel.Trace)
      .build()
    
      this.setState({ teamHubConnection }, () => {
        this.state.teamHubConnection
          .start()
          .then(() => {
            console.log('Connection started!')
            console.log("Joining Team Hub **************************** " + this.props.teamId)
            this.state.teamHubConnection.invoke("Join", this.props.gameId, this.props.teamId)
            .catch(function (err) {
                return console.error(err.toString())
            });
          })
          .catch(err => console.log('Error while establishing connection :('))

          this.state.teamHubConnection.on('Sighted', (guardId) => {
            console.log(`${this.props.teamId} SIGHTED BY ${guardId}`)
            this.get()
          });

          this.state.teamHubConnection.on('ItemFound', (itemId) => {
            console.log(`${this.props.teamId} FOUND ${itemId}`)
            this.get()
          });
      });


    }

    render() {

      return (
          <div className="player">
            <h4>Player {this.props.playerNumber}</h4>
            <div className="input">
              <input type="text" name="collectableItemId" value={this.state.collectableItemId} onChange={this.handleChange} />
              <button onClick={this.collect} >Collect Item</button>
              <button onClick={this.get} >Refresh</button>
            </div>

            <div>{this.state.message}</div>

            <Tabs>
              <div label="Remaining">
                Remaining Items {this.state.remainingItems.length}
                <ol>
                  {this.state.remainingItems.map((item, i) => 
                    <li key={i}>{item.Name}</li>
                  ) }
                </ol>
              </div>
              <div label="Sightings">
                <div>Team Sighted By Guards: {this.state.sightings.length}
                  <ol>
                    {this.state.sightings.map((item, i) => 
                      <li key={i}>{item.SightedAt} - {item.SightedBy}</li>
                      ) }
                  </ol>
                </div>
              </div>
              <div label="Found Items">
                <div>Team Collected Items {this.state.collectedItems.length}
                  <ol>
                    {this.state.collectedItems.map((item, i) => 
                      <li key={i}>{item.CollectedAt} - {item.Name}</li>
                    ) }
                  </ol>
                </div>
              </div>
            </Tabs>
        </div>
      );
    }
  }

  export default Player
  