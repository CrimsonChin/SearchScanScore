import React from 'react'

class ShowRemaining extends React.Component {
  componentDidMount(){
    this.getCollectables();
  }

  onSuccessfulDiscovery = () => {
    this.getCollectables();
  }

  getCollectables = () => {
    ActiveGameService.getCollectables("UVWMN").then((data) => {
      this.setState({collectables: data})
    })
  }

    render() {
      const photobox = {
        border: "darkgrey solid 1px",
        margin: "10px",
        borderRadius: "10px",
        padding: "10px",
        float: "left"
      }

      const qr = {
        // margin: "-30px"
      }

      const title = {
        textAlign: "center",
      }

      const subTitle = {
        textAlign: "center",
        marginBottom: "10px"
      }

      return (
        <div style={col2}>
        <h3>Available Items</h3>
        {this.state.collectables.map((item) => 
        <div style={photobox}>
          <img style={qr} src={"https://localhost:44394/api/QrCode/Generate/" + item.ExternalId} width="250" alt="lol"></img>
          <div style={title}> {item.ExternalId} </div>
          <div style={subTitle}> {item.Name} - {item.PointValue} pts </div>
          </div>
        ) }
        </div>
      );
    }
  }

  export default ShowRemaining
  