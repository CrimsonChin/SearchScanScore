import React from 'react'
import TeamService from '../../Services/TeamService'
import Tabs from '../Tabs/tabs';
require('../Tabs/styles.css');

class Player extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
          collectableItemId: "",
          sightings: [],
          collectedItems: [],
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
        this.get()
      })
    }

    get = (event) => {
      console.log("Team -> Get")
      TeamService.get(this.props.gameId, this.props.teamId).then((data) => {
        console.log(data)
        data = data || {
          ItemsCollected: [],
          Sightings: []
        }
        this.setState({
          collectedItems: data.ItemsCollected,
          sightings: data.Sightings
        })
      })
    }

    componentDidMount() {
      this.get();
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
            <Tabs>
              
              <div label="Sightings">
                <div>Team Sighted By Guards: {this.state.sightings.length}
                      <ul>
                        {this.state.sightings.map((item, i) => 
                          <li key={i}>{item.SightedAt} - {item.SightedBy}</li>
                          ) }
                      </ul>
                  </div>
              </div>
              <div label="Found Items">
                    <div>Team Collected Items {this.state.collectedItems.length}
                      <ul>
                        {this.state.collectedItems.map((item, i) => 
                          <li key={i}>{item.CollectedAt} - {item.Name}</li>
                        ) }
                      </ul>
                    </div>
              </div>
              <div label="Remaining">
                TODO: List all game items sans found ones
              </div>
            </Tabs>

 
                  


        </div>
      );
    }
  }

  export default Player
  