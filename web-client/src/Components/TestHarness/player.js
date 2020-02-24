import React from 'react'
import TeamService from '../../Services/TeamService'

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
                  
                  <div>Team Sighted By Guards: {this.state.sightings.length}
                  { this.state.sightings.length > 0 && (
                      <ul>
                        {this.state.sightings.map((item, i) => 
                          <li key={i}>{item.SightedAt} - {item.SightedBy}</li>
                          ) }
                      </ul>
                  )}
                  </div>
                  
                  <input type="text" name="collectableItemId" value={this.state.collectableItemId} onChange={this.handleChange} />
                  <button onClick={this.collect} >Collect Item</button>
                  <button onClick={this.get} >Refresh</button>
                  
                  { this.state.collectedItems.length > 0 && (
                    <div>Team Collected Items {this.state.collectedItems.length}
                      <ul>
                        {this.state.collectedItems.map((item, i) => 
                          <li key={i}>{item.CollectedAt} - {item.Name}</li>
                        ) }
                      </ul>
                    </div>
                  )}
        </div>
      );
    }
  }

  export default Player
  