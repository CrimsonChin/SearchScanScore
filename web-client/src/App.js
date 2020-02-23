import React from "react";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import Home from './Components/Home'
import Join from './Components/Join'
import Play from './Components/Play'
import Create from './Components/Create'
import Test from './Components/TestHarness'

// function Create() {
//   return <h2>Create</h2>;
// }

function AppRouter() {
  return (
    <Router>
      <div>
        <nav>
          <ul>
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/Join/">Join</Link>
            </li>
            <li>
              <Link to="/Play/">Play</Link>
            </li>
            <li>
              <Link to="/Create/">Create</Link>
            </li>
            <li>
              <Link to="/Test/">Test</Link>
            </li>
          </ul>
        </nav>

        <Route path="/" exact component={Home} />
        {/* <Route path="/Join/" component={Join} /> */}
        
        <Route path="/Join/" component={(props) => (<Join {...props} />)} />
        <Route path="/Play/" component={Play} />
        <Route path="/Create/" component={Create} />
        <Route path="/Test/" component={Test} />
      </div>
    </Router>
  );
}

export default AppRouter;
