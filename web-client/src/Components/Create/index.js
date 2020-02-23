import React from 'react'
import ActiveGameService from '../../Services/ActiveGameService'
class Create extends React.Component {
  
  constructor(props) {
    super(props);

    this.state = {
      collectables: []
    }
}
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
        marginTop: "60px"
      }

      const title = {
        textAlign: "center",
        marginBottom: "40px"
      }

      return (
        <div>
          <h2>Create</h2>
          
          <h3>Clues</h3>
          <ul>
            {this.state.collectables.map((item) => 
            <li>{item.Name}</li>
            )}
          </ul>

          <h3>Hideable Printables</h3>
          {this.state.collectables.map((item) => 
            <div style={photobox}>
              <img style={qr} src={"https://localhost:44394/api/QrCode/Generate/" + item.ExternalId} width="250" alt="lol"></img>
              <div style={title}> {item.ExternalId}</div>
            </div>
          ) }
        </div>
      );
    }
  }

  export default Create
  