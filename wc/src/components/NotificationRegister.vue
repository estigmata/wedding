<template>
  <div class="row">
    <div class="col-sm-6 text-center margin-center jumbotron center-y-center-x border-rad">
      <div>
        <h3 class="text"> Hi {{this.account.firstName}} and {{this.account.firstNamePartner}} </h3>
      </div>
      <div class="row">
        <label class="col-sm-12 text-success margin-center font-weight-bold"> Your registration was successful </label>
      </div>
      <div class="row">
        <label class="col-sm-6 font-weight-bold"> Account: </label>
        <label class="col-sm-6"> {{this.account.account}} </label>
      </div>
      <div class="row">
        <label class="col-sm-6 font-weight-bold"> Password: </label>
        <label class="col-sm-6"> {{this.account.password}} </label>
      </div>
      <div>
        <button v-on:click="goToHome" type="button" class="btn btn-info bg-primary"> Go to Homepage </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data () {
    return {
      account: this.$route.params.acc
    }
  },
  methods: {
    goToHome () {
      this.$router.push({name: 'searchcouplelist'})
    }
  }
}
</script>

<style scoped>
  .row {
    margin: 0;
  }
  .text-center {
    text-align: center;
  }
  .margin-center {
    margin-left: auto;
    margin-right: auto;
  }
  .center-y-center-x {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
  }
  .text-white {
    color: white;
  }
  .margin-top {
    margin-top: 10px;
  }
  .bg-notification {
    background-color: #d9d9d9;
  }
  .border-rad {
    border-radius: 20px;
    border: 2px solid black;
  }
</style>
