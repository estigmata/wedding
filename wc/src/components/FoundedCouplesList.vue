<template>
  <div class="result-list">
    <div v-if="couplesNumber === '1'" class="results">
      <div v-for='(coupleList, index) in coupleLists' :key='index'>
        <div class="card w-100">
          <div class="card-body">
            <h5 class="card-title">{{coupleList.husband.name +' '+ coupleList.husband.lastName +' and '+ coupleList.wife.name + ' ' + coupleList.wife.lastName }}</h5>
            <div class="row" v-if="getStatus(coupleList.status)">
              <div class="col-8">
                <p class="card-text">Wedding Date: {{convertDate(coupleList.weddingDate)}}</p>
                <p class="card-text success">{{coupleList.status}}</p>
              </div>
              <div class="col-4">
                <a href="#" class="btn btn-primary"  @click="gotoList(coupleList.presentListId, coupleList.husband.name, coupleList.wife.name)"  >Go to the List</a>
              </div>
            </div>
            <div class="row" v-else>
              <div class="col-12">
                <p class="card-text">Wedding Date: {{convertDate(coupleList.weddingDate)}}</p>
                <p class="card-text error">{{coupleList.status}}</p>
              </div>
            </div>
          </div>
        </div>
        <div class="couple-names"></div>
      </div>
    </div>
    <div v-else-if="couplesNumber === '0'">
      <b-alert show variant="danger">NO RESULTS MATCH</b-alert>
    </div>
  </div>
</template>

<script>
import PresentListResource from '../resources/PresentListResource'
export default {
  data () {
    return {
      coupleLists: [],
      list: {},
      couplesNumber: '-1'
    }
  },
  methods: {
    search: function (name, lastName) {
      PresentListResource.SearchPresentList(name, lastName).then((result) => {
        this.coupleLists = result['body']
        this.couplesNumber = '1'
      })
        .catch(error => {
          if (error.status === 404) {
            this.couplesNumber = '0'
            console.log(error)
          }
        })
    },
    gotoList: function (presentListId, husband, wife) {
      let str = '/present-list/' + husband + '/' + wife + '/' + presentListId
      this.$router.push({path: str})
    },
    getStatus: function (status) {
      let array = status.split(':')
      if (array[0] === 'Available') {
        return true
      } else {
        return false
      }
    },
    convertDate: function (inputFormat) {
      function pad (s) {
        return (s < 10) ? '0' + s : s
      }
      var d = new Date(inputFormat)
      return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/')
    }
  }
}

</script>

<style scoped>
.result-list{
  margin-top: 10px;
  text-align: center;
  width: 100%;
  height: auto;
}
.results{
  height: 280px;
  overflow-y: auto;
  overflow-x: hidden;
}

.card-text{
  margin: 1px;
  font-size: 12px;
}

.card-body{
  padding: 0.5rem;
}

.error{
  color: red;
}

.success{
  color: green;
}

</style>
