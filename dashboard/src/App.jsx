import { useEffect, useState } from 'react'
import { getAllData } from './store/data'
import CapstoneChart from './components/capstonechart'

function App() {
  const [data, setData] = useState ([])

  useEffect (() => {
    async function fetchData () {
      let response = await getAllData ();
      setData (response);  
    };

    fetchData();
  }, []);

  return (
      <CapstoneChart 
        data={data}
      />
  )
}

export default App
