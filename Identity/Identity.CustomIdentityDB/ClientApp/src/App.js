import "./App.css";
import SidePanel from "./components/SidePanel";
import Content from "./components/Content"

function App() {
  return (
    <div className="frame">
      <SidePanel/>

      <div></div>

      <Content/>
    </div>
  );
}

export default App;
