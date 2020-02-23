import React from 'react'
import TeamService from '../../Services/TeamService'
import Player from './player'

class TeamHarness extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
          hasJoinedTeam: false,
          collectableItemId: "",
          remainingItems: [],
          collectedItems: [],
          showRemaingItemExternalIds: true,
          players: []
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

    canJoinTeam = (event) => {
      console.log("Can Join Team?")
      TeamService.canJoinTeam(this.props.gameId, this.props.teamId).then((data) => {
        console.log("Has joined team: " + data)
        this.setState({
          hasJoinedTeam: data,
          players: [...this.state.players, {}]
        })

        this.collected();
      })
    }

    render() {

      return (
          <div className="team">
            <h3>{this.props.teamName}</h3>
            <h4>Game Id: {this.props.gameId} Team Id: {this.props.teamId}  <button onClick={this.canJoinTeam}>Add Player</button> </h4>
              {this.state.players.map((item, i) => 
                <Player gameId={this.props.gameId} teamId={this.props.teamId} playerNumber={i + 1}></Player>
              ) }
        </div>
      );
    }
  }

  export default TeamHarness
  