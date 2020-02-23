import React from 'react'
import TeamService from '../../Services/TeamService'

class Player extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
          collectableItemId: "",
          remainingItems: [],
          collectedItems: [],
          showRemaingItemExternalIds: true
        }
    }

    handleChange = (event) => {
      this.setState({collectableItemId: event.target.value});
    }

    handleChangeChk = (event) => {
      this.setState({showRemaingItemExternalIds: !this.state.showRemaingItemExternalIds})
    }

    collect = (event) => {
      console.log("Collecting Item: ", this.state.collectableItemId)
      TeamService.collectItem(this.props.gameId, this.props.teamId, this.state.collectableItemId).then((data) => {
        console.log(data)
        this.setState({
          remainingItems: data.RemainingCollectableItems,
          collectedItems: data.ItemsCollected,
          collectableItemId: ""
        })
      })
    }

    collected = (event) => {
      console.log("Getting Collected Items")
      TeamService.getCollectedItems(this.props.gameId, this.props.teamId).then((data) => {
        console.log(data)
        data = data || {
          RemainingCollectableItems : [],
          ItemsCollected: []
        }
        this.setState({
          remainingItems: data.RemainingCollectableItems,
          collectedItems: data.ItemsCollected
        })
      })
    }

    componentDidMount() {
      this.collected();
    }

    render() {

      return (
          <div className="player">
            <h4>Player {this.props.playerNumber}</h4>
                  <input type="text" name="collectableItemId" value={this.state.collectableItemId} onChange={this.handleChange} />
                  <button onClick={this.collect} >Collect Item</button>
                  <button onClick={this.collected} >Get Collected Items (Refresh)</button>
                  <br/>
                  <input type="checkbox" name="showRemaingItemExternalIds" defaultChecked={this.state.showRemaingItemExternalIds} onChange={this.handleChangeChk} />
                  <label htmlFor="showRemaingItemExternalIds">Show External Ids</label>
                  
                  { this.state.remainingItems.length > 0 && (
                    <div>Remaining Items {this.state.remainingItems.length} / {this.state.collectedItems.length  + this.state.remainingItems.length} 
                      <ul>
                        {this.state.remainingItems.map((item, i) => 
                          <li key={i}>{this.state.showRemaingItemExternalIds && ( <> {item.ExternalId} - </> )}{item.Name}</li>
                        ) }
                      </ul>
                    </div>
                  )}
                  
                  { this.state.collectedItems.length > 0 && (
                    <div>Collected Items {this.state.collectedItems.length} / {this.state.collectedItems.length  + this.state.remainingItems.length}
                      <ul>
                        {this.state.collectedItems.map((item, i) => 
                          <li key={i}>{item.CollectedAt} - {this.state.showRemaingItemExternalIds && ( <>{item.ExternalId} - </> )}{item.Name}</li>
                        ) }
                      </ul>
                    </div>
                  )}
        </div>
      );
    }
  }

  export default Player
  