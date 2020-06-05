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
      TeamService.addCollectedItem(this.props.gameId, this.props.teamId, this.state.collectableItemId).then((data) => {
        console.log(data)
        this.setState({
          collectableItemId: ""
        })
      })
    }

    get = (event) => {
      TeamService.getRemainingCollectableItems(this.props.gameId, this.props.teamId).then((data) => {
        console.log("collectable/remaining", data)
        data = data || {
          remainingItems: [],
        }
        this.setState({
          remainingItems: data
        })
      })

      TeamService.getCollectedItems(this.props.gameId, this.props.teamId).then((data) => {
        console.log("collected", data)
        data = data || {
          itemsCollected: [],
        }

        this.setState({
          collectedItems: data,
        })
      })

      TeamService.getSightings(this.props.gameId, this.props.teamId).then((data) => {
        console.log("sightings", data)
        data = data || {
          sightings: []
        }
        this.setState({
          sightings: data
        })
      })
    }

    componentDidMount = () => {
      this.get()
      
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

          this.state.teamHubConnection.on('AdminMessage', (message) => {
            const text = `ADMIN MESSAGE: ${message}`;
            
            this.setState({ 
              message: text
             });
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
                    <li key={i}>{item.name}</li>
                  ) }
                </ol>
              </div>
              <div label="Sightings">
                <div>Team Sighted By Guards: {this.state.sightings.length}
                  <ol>
                    {this.state.sightings.map((item, i) => 
                      <li key={i}>{item.sightedAt} - {item.sightedBy}</li>
                      ) }
                  </ol>
                </div>
              </div>
              <div label="Found Items">
                <div>Team Collected Items {this.state.collectedItems.length}
                  <ol>
                    {this.state.collectedItems.map((item, i) => 
                      <li key={i}>{item.collectedAt} - {item.collectableItemName}</li>
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
  